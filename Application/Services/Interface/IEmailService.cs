using Application.DTO.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interface;
public interface IEmailService
{
    Task SendEmail(SendEmailDTO dto);
}
