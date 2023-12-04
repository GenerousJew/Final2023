using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Utilizer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsBusy { get; set; }

    public bool IsResearch { get; set; }
}
