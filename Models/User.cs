using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellMI.Models;

public partial class User
{
    [Key]
     public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string EmailId { get; set; } = null!;

    public string? Password { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsActive { get; set; }

    public int? UserType { get; set; }

    public bool? IsVerify { get; set; }

    
}
