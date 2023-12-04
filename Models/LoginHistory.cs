using System;
using System.Collections.Generic;

namespace UtilizerAPI.Models;

public partial class LoginHistory
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public DateTime Date { get; set; }

    public bool Success { get; set; }
}
