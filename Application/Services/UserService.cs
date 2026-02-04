using Application.DTO.User;
using Application.Services.Interface;
using Domain.Models;
using Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDTO> CreateUser(CreateUserDTO dto)
    {
        var user = new User(dto.Username, dto.Password);
        await _repository.AddUser(user);
        return new UserDTO(user.Id, user.Username);
    }

    public async Task DeleteUser(Guid id)
    {
        await _repository.DeleteUser(id);
    }

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var users = await _repository.GetAll();
        return users.Select(u => new UserDTO(
                u.Id,
                u.Username
            )).ToList();
    }

    public async Task<UserDTO> GetById(Guid id)
    {
        var user = await _repository.GetById(id) 
            ?? throw new Exception("Usuário não encontrado");
        return new UserDTO(user.Id, user.Username);
    }

    public async Task<UserDTO> UpdateUser(Guid id, UpdateUserDTO dto)
    {
        var user = await _repository.GetById(id)
            ?? throw new Exception("Usuário não encontrado");

        user.ChangeUsername(dto.Username);
        user.ChangePassword(dto.Password);

        await _repository.UpdateUser();
        return new UserDTO(user.Id, user.Username);
    }
}
