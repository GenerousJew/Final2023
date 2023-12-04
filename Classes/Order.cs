using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int Status { get; set; }

    public int? ExecutionTime { get; set; }

    public int? Number { get; set; }

    public int Client { get; set; }

    public virtual Client ClientNavigation { get; set; } = null!;

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual Status StatusNavigation { get; set; } = null!;
}
