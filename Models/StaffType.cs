using System;
using System.Collections.Generic;

namespace FinalAPI.Models;

public partial class StaffType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
