using Application.DTO.User;
using Application.Services.Interface;
using Domain.Models;
using Infrastructure.Exporter;
using Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services;

public class FakeDataGenerationService : IFakeDataGenerationService
{
    public Stream GenerateMany(int quantity)
    {
        var fakeUsers = RandomUserFactory.Generate(quantity);
        Stream excel = ExcelExporter.Export(fakeUsers);
        return excel;
    }
}
