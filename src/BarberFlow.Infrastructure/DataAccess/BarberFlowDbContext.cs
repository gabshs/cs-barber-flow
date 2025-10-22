using BarberFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberFlow.Infrastructure.DataAccess;

internal class BarberFlowDbContext : DbContext
{
    public BarberFlowDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Billing> Billings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Billing>(entity =>
        {
            entity.Property(e => e.PaymentMethod)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);
        });

        base.OnModelCreating(modelBuilder);
    }
}