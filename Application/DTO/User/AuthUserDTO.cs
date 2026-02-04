using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.User;

public record AuthUserDTO(
    string Username,
    string Password
);