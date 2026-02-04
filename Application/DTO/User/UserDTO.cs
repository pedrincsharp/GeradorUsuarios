using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.User;
public record UserDTO(
    Guid Id,
    string Username
);