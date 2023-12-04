using System;
using System.Collections.Generic;

namespace FinalAPI.Models;

public partial class Service
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public TimeSpan? ExecutionPeriod { get; set; }

    public decimal? AverageDeviation { get; set; }

    public decimal? AverageResult { get; set; }

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<Utilizer> Utilizers { get; set; } = new List<Utilizer>();
}
