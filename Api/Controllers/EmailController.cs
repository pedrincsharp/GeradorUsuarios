using Application.DTO.Email;
using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmailController : BaseApiController
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromForm] SendEmailDTO dto)
    {
        try
        {
            await _emailService.SendEmail(dto);
            return SuccessNoContent();
        }
        catch (Exception ex)
        {
            return Error(ex.Message, statusCode: (int)(HttpStatusCode.InternalServerError));
        }
    }
}
