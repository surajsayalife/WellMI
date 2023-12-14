using System;
using System.Collections.Generic;

namespace WellMI.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Password { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? Gender { get; set; }

    public int? ParentId { get; set; }

    public bool? IsActive { get; set; }
}
