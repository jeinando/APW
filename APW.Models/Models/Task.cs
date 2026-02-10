using System;
using System.Collections.Generic;
using APW.Models.Entities;


namespace APW.Models;

public partial class Task : IEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    public string? ModifiedBy { get; set; }
}
