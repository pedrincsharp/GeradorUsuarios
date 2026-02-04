using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interface;

public interface IFakeDataGenerationService
{
    Stream GenerateMany(int quantity);
}