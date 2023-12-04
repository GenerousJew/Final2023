using System;
using System.Collections.Generic;

namespace FinalAPI.Models;

public partial class Result
{
    public int OrderServiceId { get; set; }

    public decimal Result1 { get; set; }

    public virtual OrderService OrderService { get; set; } = null!;
}
