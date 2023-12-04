using System;
using System.Collections.Generic;

namespace FinalAPI.Models;

public partial class Status
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
