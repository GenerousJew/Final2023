using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Itn { get; set; }

    public string? PaymentAccount { get; set; }

    public string? Bic { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
