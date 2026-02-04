using Application.Services;
using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FakeDataGenerationController : BaseApiController
{
    private readonly IFakeDataGenerationService _fakeDataGenerationService;

    public FakeDataGenerationController(IFakeDataGenerationService fakeDataGenerationService)
    {
        _fakeDataGenerationService = fakeDataGenerationService;
    }

    [HttpGet("{quantity}")]
    public async Task<IActionResult> GenerateMany(int quantity)
    {
        try
        {
            var fakeUsers = _fakeDataGenerationService.GenerateMany(quantity);

            return File(
                fakeUsers,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "relatorio.xlsx"
            );
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