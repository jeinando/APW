using System;
using System.Collections.Generic;
using APW.Models.Entities;

namespace APW.Models;

public partial class User : IEntity
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? LastModified { get; set; }

    public string? ModifiedBy { get; set; }
}
