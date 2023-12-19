using System;
using System.Collections.Generic;

namespace WellMI.Models;

public partial class Employer
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? CompanyName { get; set; }

    public int UserRegistrationInviteCode { get; set; }

    public int EmployerRegistrationInviteCode { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual User? User { get; set; }
}
