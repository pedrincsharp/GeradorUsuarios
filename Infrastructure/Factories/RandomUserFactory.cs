using Bogus;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Factories;

public static class RandomUserFactory
{
    public static List<User> Generate(int quantity)
    {
        var faker = new Faker<User>("pt_BR")
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => f.Internet.Password());

        return faker.Generate(quantity);
    }
}