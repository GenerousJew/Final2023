using System;
using System.Collections.Generic;

namespace UtilizerAPI.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Lastenter { get; set; }

    public int? Type { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual StaffType? TypeNavigation { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
