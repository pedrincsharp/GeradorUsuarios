using Domain.Models;
using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interface;
public interface ITokenService
{
    TokenDTO Generate(User user);
}
