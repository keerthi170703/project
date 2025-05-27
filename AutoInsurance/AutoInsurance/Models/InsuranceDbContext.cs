using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AutoInsurance.Models;

public partial class InsuranceDbContext : DbContext
{
    public InsuranceDbContext()
    {
    }

    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<SupportTicket> SupportTickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=LTIN537186\\SQLEXPRESS01;Database=InsuranceDB;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("PK__Claim__01BDF9D35C747598");

            entity.ToTable("Claim");

            entity.Property(e => e.ClaimId).HasColumnName("claimId");
            entity.Property(e => e.AdjusterId).HasColumnName("adjusterId");
            entity.Property(e => e.ClaimAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("claimAmount");
            entity.Property(e => e.ClaimDate).HasColumnName("claimDate");
            entity.Property(e => e.ClaimStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("claimStatus");
            entity.Property(e => e.PolicyId).HasColumnName("policyId");

            entity.HasOne(d => d.Adjuster).WithMany(p => p.Claims)
                .HasForeignKey(d => d.AdjusterId)
                .HasConstraintName("FK__Claim__adjusterI__571DF1D5");

            entity.HasOne(d => d.Policy).WithMany(p => p.Claims)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__Claim__policyId__5629CD9C");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__A0D9EFC63328F462");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("paymentId");
            entity.Property(e => e.PaymentAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("paymentAmount");
            entity.Property(e => e.PaymentDate).HasColumnName("paymentDate");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("paymentStatus");
            entity.Property(e => e.PolicyId).HasColumnName("policyId");

            entity.HasOne(d => d.Policy).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__Payment__policyI__628FA481");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policy__78E3A9225E8B2621");

            entity.ToTable("Policy");

            entity.Property(e => e.PolicyId).HasColumnName("policyId");
            entity.Property(e => e.CoverageAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("coverageAmount");
            entity.Property(e => e.CoverageType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("coverageType");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("policyNumber");
            entity.Property(e => e.PolicyStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("policyStatus");
            entity.Property(e => e.PremiumAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("premiumAmount");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.VehicleDetails)
                .HasColumnType("text")
                .HasColumnName("vehicleDetails");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__SupportT__3333C610B0B7D98A");

            entity.ToTable("SupportTicket");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");
            entity.Property(e => e.CreatedDate).HasColumnName("createdDate");
            entity.Property(e => e.IssueDescription)
                .HasColumnType("text")
                .HasColumnName("issueDescription");
            entity.Property(e => e.ResolvedDate).HasColumnName("resolvedDate");
            entity.Property(e => e.TicketStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ticketStatus");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.SupportTickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__SupportTi__userI__5EBF139D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CFFC1BE94E1");

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5722083E2FA").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
