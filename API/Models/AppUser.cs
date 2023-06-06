using System;
using System.Collections.Generic;

namespace API.Models;

public partial class AppUser
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public byte[] Password { get; set; }
    public byte[] PasswordSalt { get; set; }

}
