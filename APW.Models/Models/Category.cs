using System;
using System.Collections.Generic;
using APW.Models.Entities;

namespace APW.Models;

public partial class Category : IEntity
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public DateTime? LastModified { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
