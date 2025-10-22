using BarberFlow.Communication.Enums;

namespace BarberFlow.Communication.Requests;

public class RequestUpdateBillingJson
{
    public string? BarberName { get; set; }
    public string? ClientName { get; set; }
    public string? ServiceName { get; set; }
    public DateOnly? Date { get; set; }
    public decimal? Amount { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public BillingStatus? Status { get; set; }
    public string? Notes { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
