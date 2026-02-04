using Application.DTO.User;
using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAll();
                return Success(users);
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
        {
            try
            {
                var user = await _userService.CreateUser(dto);
                return Success(user, statusCode: (int)HttpStatusCode.Created);
            }
            catch(ArgumentException argumentEx)
            {
                return Error(argumentEx.Message);
            }
            catch (Exception ex)
            {
                return Error(ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id,[FromBody] UpdateUserDTO dto)
        {
            try
            {
                var user = await _userService.UpdateUser(id, dto);
                return Success(user);
            }
            catch(ArgumentException argumentEx)
            {
                return Error(argumentEx.Message);
            }
            catch (Exception ex)
            {
                return Error(ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return SuccessNoContent();
            }
            catch(ArgumentException argumentEx)
            {
                return Error(argumentEx.Message);
            }
            catch (Exception ex)
            {
                return Error(ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var user = await _userService.GetById(id);
                return Success(user);
            }
            catch (Exception ex)
            {
                return Error(ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
