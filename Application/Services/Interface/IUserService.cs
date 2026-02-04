using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interface;
public interface IUserService
{
    Task<UserDTO> CreateUser(CreateUserDTO dto);
    Task<UserDTO> UpdateUser(Guid id, UpdateUserDTO dto);
    Task DeleteUser(Guid id);
    Task<UserDTO> GetById(Guid id);
    Task<IEnumerable<UserDTO>> GetAll();
}