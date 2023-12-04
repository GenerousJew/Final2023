using System;
using System.Collections.Generic;

namespace UtilizerAPI.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}
