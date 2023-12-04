using System;
using System.Collections.Generic;

namespace FinalAPI.Models;

public partial class Utilizer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsBusy { get; set; }

    public bool IsResearch { get; set; }

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
