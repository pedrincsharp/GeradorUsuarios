using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(16);
            entity.HasIndex(u => u.Username).IsUnique();
        });

        var testUsername = _configuration["Test:Username"]!;
        var testPassword = _configuration["Test:Password"]!;

        var testUser = new User(testUsername, testPassword);

        modelBuilder.Entity<User>().HasData(testUser);
    }
}