using System;
using System.Collections.Generic;

namespace API.Models;

public partial class OrderService
{
    public int Id { get; set; }

    public int Order { get; set; }

    public int Service { get; set; }

    public DateTime? StartTime { get; set; }

    public int? ExecutionTime { get; set; }

    public int? Status { get; set; }

    public int? Staff { get; set; }

    public int? Utilizer { get; set; }

    public virtual Order OrderNavigation { get; set; } = null!;

    public virtual Service ServiceNavigation { get; set; } = null!;

    public virtual Staff? StaffNavigation { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual Utilizer? UtilizerNavigation { get; set; }
}
