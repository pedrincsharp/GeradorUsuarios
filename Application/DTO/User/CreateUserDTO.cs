using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.User;
public record CreateUserDTO(
    string Username,
    string Password
);