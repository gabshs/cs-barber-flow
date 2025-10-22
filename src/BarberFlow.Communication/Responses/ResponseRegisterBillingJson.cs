namespace BarberFlow.Communication.Responses;

public class ResponseRegisterBillingJson
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

}
