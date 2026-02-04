using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository.Interface;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(Guid id);
    Task<User> AddUser(User model);
    Task UpdateUser();
    Task DeleteUser(Guid id);
    Task<User?> GetByUsername(string username);
}