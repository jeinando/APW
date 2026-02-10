using System;
using System.Collections.Generic;
using APW.Models.Entities;


namespace APW.Models;

public partial class Notification : IEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }
}
