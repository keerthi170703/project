using System;
using System.Collections.Generic;

namespace AutoInsurance.Models;

public partial class Claim
{
    public int ClaimId { get; set; }

    public int? PolicyId { get; set; }

    public decimal? ClaimAmount { get; set; }

    public DateOnly? ClaimDate { get; set; }

    public string? ClaimStatus { get; set; }

    public int? AdjusterId { get; set; }

    public virtual User? Adjuster { get; set; }

    public virtual Policy? Policy { get; set; }
}
