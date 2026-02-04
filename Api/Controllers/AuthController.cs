using Application.DTO.User;
using Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost()]
    public async Task<IActionResult> Login([FromBody] AuthUserDTO dto)
    {
        try
        {
            var token = await _authService.Login(dto);
            return Success(token);
        }
        catch (ArgumentException argumentEx)
        {
            return Error(argumentEx.Message);
        }
        catch (Exception ex)
        {
            return Error(ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
        }
    }
}
