using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult Success<T>(T data, string message = "Operação realizada com sucesso", int statusCode = 200)
    {
        var response = new APIResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            Timestamp = DateTime.UtcNow
        };
        return StatusCode(statusCode, response);
    }

    protected IActionResult SuccessNoContent(string message = "Operação realizada com sucesso")
    {
        var response = new APIResponse<object>
        {
            Success = true,
            Message = message,
            Data = null,
            Timestamp = DateTime.UtcNow
        };
        return Ok(response);
    }

    protected IActionResult SuccessCreated<T>(T data, string message = "Recurso criado com sucesso") 
        => Success(data, message, 201);

    protected IActionResult SuccessAccepted<T>(T data, string message = "Operação aceita para processamento") 
        => Success(data, message, 202);


    protected IActionResult Error(string message, List<string> errors = null, int statusCode = 400)
    {
        var response = new APIResponse<object>
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>(),
            Timestamp = DateTime.UtcNow
        };
        return StatusCode(statusCode, response);
    }

    protected IActionResult Error<T>(T data, string message, List<string> errors = null, int statusCode = 400)
    {
        var response = new APIResponse<T>
        {
            Success = false,
            Message = message,
            Data = data,
            Errors = errors ?? new List<string>(),
            Timestamp = DateTime.UtcNow
        };
        return StatusCode(statusCode, response);
    }

    protected IActionResult NotFoundError(string message = "Recurso não encontrado")
    {
        return Error(message, statusCode: 404);
    }

    protected IActionResult UnauthorizedError(string message = "Não autorizado")
    {
        return Error(message, statusCode: 401);
    }

    protected IActionResult ForbiddenError(string message = "Acesso negado")
    {
        return Error(message, statusCode: 403);
    }

    protected IActionResult ValidationError(string message, List<string> errors)
    {
        return Error(message, errors, statusCode: 422);
    }

    protected IActionResult ConflictError(string message, List<string> errors = null)
    {
        return Error(message, errors, statusCode: 409);
    }

    protected IActionResult InternalServerError(string message = "Erro interno do servidor", List<string> errors = null)
    {
        return Error(message, errors, statusCode: 500);
    }
}