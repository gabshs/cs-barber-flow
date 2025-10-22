using BarberFlow.Communication.Responses;

namespace BarberFlow.Application.UseCases.Billings.GetById;

public interface IGetBillingByIdUseCase
{
    Task<ResponseBillingJson> ExecuteAsync(Guid billingId);

}
