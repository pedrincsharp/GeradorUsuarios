using Application.Services.Interface;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDTO Generate(User user)
    {
        var secret = _configuration["Jwt:Secret"] 
            ?? throw new Exception("Secret não informada nas configurações");

        var expiration = _configuration.GetValue<int>("Jwt:Expiration");
        if (expiration == 0)
            throw new Exception("Tempo de expiração não informado nas configurações");

        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
            }),
            Expires = DateTime.UtcNow.AddHours(expiration),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(tokenDescriptor);
        
        return new TokenDTO()
        {
            AccessToken = handler.WriteToken(token),
            ExpiresAt = tokenDescriptor.Expires.Value
        };
    }
}
