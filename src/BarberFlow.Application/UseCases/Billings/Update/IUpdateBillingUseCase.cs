using BarberFlow.Communication.Requests;

namespace BarberFlow.Application.UseCases.Billings.Update;

public interface IUpdateBillingUseCase
{
    Task ExecuteAsync(Guid id, RequestRegisterBillingJson request);

}
