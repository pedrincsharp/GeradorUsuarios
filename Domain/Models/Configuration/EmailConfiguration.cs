using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Configuration;

public class EmailConfiguration
{
    public string SmtpServer { get; set; } = string.Empty;
    public string SmtpEmail { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public bool SmtpEnableSsl { get; set; }
    public string Recipient { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(SmtpServer)
            && !string.IsNullOrWhiteSpace(SmtpEmail)
            && !string.IsNullOrWhiteSpace(SmtpPassword)
            && SmtpPort > 0
            && !string.IsNullOrWhiteSpace(Recipient);
    }
}
