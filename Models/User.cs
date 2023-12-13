using System;
using System.Collections.Generic;

namespace WellMI.Models;

public partial class User
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int Gender { get; set; }

    public int? ParentId { get; set; }

    public int? RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool IsDeleted { get; set; }
}
