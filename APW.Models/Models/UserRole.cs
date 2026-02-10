using System;
using System.Collections.Generic;
using APW.Models.Entities;

namespace APW.Models;

public partial class UserRole : IEntity
{
    public decimal? Id { get; set; }

    public decimal? RoldId { get; set; }

    public decimal? UserId { get; set; }
}
