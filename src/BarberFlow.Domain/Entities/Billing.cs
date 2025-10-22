using System.ComponentModel.DataAnnotations;
using BarberFlow.Domain.Enums;

namespace BarberFlow.Domain.Entities;

public class Billing
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; set; }
    [Required, StringLength(80, MinimumLength = 2)]
    public string BarberName { get; set; } = string.Empty;
    [Required, StringLength(120, MinimumLength = 2)]
    public string ClientName { get; set; } = string.Empty;
    [Required, StringLength(120, MinimumLength = 2)]
    public string ServiceName { get; set; } = string.Empty;
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }
    [Required, EnumDataType(typeof(PaymentMethod))]
    public PaymentMethod PaymentMethod { get; set; }
    [Required, EnumDataType(typeof(BillingStatus))]
    public BillingStatus Status { get; set; }
    [StringLength(500)]
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

}
