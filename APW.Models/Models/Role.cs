using System;
using System.Collections.Generic;
using APW.Models.Entities;

namespace APW.Models;

public partial class Role : IEntity
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }
}
