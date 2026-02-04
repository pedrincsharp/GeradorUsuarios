using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public User() { }
    public User(Guid id,string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
    public User(string username, string password)
    {
        Id = Guid.CreateVersion7();
        Username = username;
        Password = password;
    }

    public void ChangeUsername(string value) => Username = value;
    public void ChangePassword(string value) => Password = value;
}