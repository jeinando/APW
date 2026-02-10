using System;
using System.Collections.Generic;
using APW.Models.Entities;


namespace APW.Models;

public partial class UserAction : IEntity
{
    public decimal? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}




