using System;
using System.Collections.Generic;

namespace UtilizerAPI.Models;

public partial class SocailType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
