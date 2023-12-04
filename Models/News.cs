using System;
using System.Collections.Generic;

namespace UtilizerAPI.Models;

public partial class News
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Text { get; set; } = null!;
}
