using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Application.DTO.Email;
public record SendEmailDTO(
    IFormFile attachment,
    string fileName
);
