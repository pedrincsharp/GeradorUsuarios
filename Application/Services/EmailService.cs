using Application.DTO.Email;
using Application.Services.Interface;
using Domain.Models.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public EmailConfiguration LoadEmailConfiguration()
    {
        var emailConfig = new EmailConfiguration
        {
            SmtpServer = _configuration.GetValue<string>("Email:SmtpServer")
                ?? throw new ArgumentException("Servidor SMTP não configurado. Verifique 'Email:SmtpServer' no appsettings.json"),
            SmtpEmail = _configuration.GetValue<string>("Email:SmtpEmail")
                ?? throw new ArgumentException("Email do SMTP não configurado. Verifique 'Email:SmtpEmail' no appsettings.json"),
            SmtpPassword = _configuration.GetValue<string>("Email:SmtpPassword")
                ?? throw new ArgumentException("Senha do SMTP não configurada. Verifique 'Email:SmtpPassword' no appsettings.json"),
            SmtpPort = _configuration.GetValue<int>("Email:SmtpPort"),
            SmtpEnableSsl = _configuration.GetValue<bool>("Email:SmtpEnableSsl"),
            Recipient = _configuration.GetValue<string>("Email:Recipient")
                ?? throw new ArgumentException("Destinatário não configurado. Verifique 'Email:Recipient' no appsettings.json"),
            Subject = _configuration.GetValue<string>("Email:Subject") ?? string.Empty,
            Body = _configuration.GetValue<string>("Email:Body") ?? string.Empty
        };

        if (!emailConfig.IsValid())
            throw new ArgumentException("Configurações de email inválidas. Verifique todos os valores necessários no appsettings.json");

        return emailConfig;
    }

    public async Task SendEmail(SendEmailDTO dto)
    {
        var config = LoadEmailConfiguration();

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("", config.SmtpEmail));
        message.To.Add(new MailboxAddress("", config.Recipient));
        message.Subject = config.Subject;

        var builder = new BodyBuilder
        {
            TextBody = config.Body
        };

        if (dto.attachment != null)
        {
            using var stream = new MemoryStream();
            await dto.attachment.CopyToAsync(stream);
            stream.Position = 0;

            builder.Attachments.Add(
                dto.fileName,
                stream.ToArray(),
                ContentType.Parse("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            );
        }

        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            var secureSocketOptions = config.SmtpEnableSsl
                ? SecureSocketOptions.StartTls
                : SecureSocketOptions.None;

            await client.ConnectAsync(config.SmtpServer, config.SmtpPort, secureSocketOptions);
            await client.AuthenticateAsync(config.SmtpEmail, config.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao enviar email: {ex.Message}", ex);
        }
    }
}