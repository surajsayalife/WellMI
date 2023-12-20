using System;
using System.Collections.Generic;

namespace WellMI.Models;

public partial class Employer
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? CompanyName { get; set; }

    public string UserRegistrationInviteCode { get; set; }

    public string EmployerRegistrationInviteCode { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    
}
