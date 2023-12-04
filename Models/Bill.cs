using System;
using System.Collections.Generic;

namespace FinalAPI.Models;

public partial class Bill
{
    public int Id { get; set; }

    public int? Staff { get; set; }

    public int? Company { get; set; }

    public double? Amount { get; set; }

    public virtual Company? CompanyNavigation { get; set; }

    public virtual Staff? StaffNavigation { get; set; }
}
