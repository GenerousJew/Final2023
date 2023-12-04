using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? PasSeries { get; set; }

    public string? PasNumber { get; set; }

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public int? Company { get; set; }

    public virtual Company? CompanyNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
