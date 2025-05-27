using System;
using System.Collections.Generic;

namespace AutoInsurance.Models;

public partial class Policy
{
    public int PolicyId { get; set; }

    public string PolicyNumber { get; set; } = null!;

    public string? VehicleDetails { get; set; }

    public decimal? CoverageAmount { get; set; }

    public string? CoverageType { get; set; }

    public decimal? PremiumAmount { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? PolicyStatus { get; set; }

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
