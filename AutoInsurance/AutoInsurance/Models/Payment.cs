using System;
using System.Collections.Generic;

namespace AutoInsurance.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? PolicyId { get; set; }

    public decimal? PaymentAmount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual Policy? Policy { get; set; }
}
