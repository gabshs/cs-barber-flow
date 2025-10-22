using System;

namespace BarberFlow.Communication.Responses;

public class ResponseListBillingsJson
{
    public Guid Id { get; set; }
    public string BarberName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
