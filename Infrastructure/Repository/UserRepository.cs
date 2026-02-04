using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    public async Task<User> AddUser(User model)
    {
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await GetById(id);
        if (user == null)
            throw new Exception("Usuário não encontrado");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task UpdateUser()
    {
        await _context.SaveChangesAsync();
    }
}