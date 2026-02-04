using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.User;
public class TokenDTO
{
    public string AccessToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}