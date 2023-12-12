using System;
using System.Collections.Generic;

namespace WellMI.Models;

public partial class User
{
    public  long Id { get; set; }

    public string? Username { get; set; }

    public string EmailId { get; set; } = null!;

    public string? Password { get; set; }

    public int? AccountType { get; set; }

    //public string Token { get; set; } 
}
