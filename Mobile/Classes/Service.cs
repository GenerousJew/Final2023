using System;
using System.Collections.Generic;

public partial class Service
{
    public int code { get; set; }

    public string name { get; set; }

    public double price { get; set; }

    public TimeSpan? executionPeriod { get; set; }

    public TimeSpan? averageDeviation { get; set; }
}
