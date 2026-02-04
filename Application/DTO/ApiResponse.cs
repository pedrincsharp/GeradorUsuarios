using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO;

public class APIResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; }
    public DateTime Timestamp { get; set; }

    public APIResponse()
    {
        Errors = new List<string>();
    }
}