using BarberFlow.Communication.Requests;
using BarberFlow.Communication.Responses;

namespace BarberFlow.Application.UseCases.Billings.Register;

public interface IRegisterBillingUseCase
{
    Task<ResponseRegisterBillingJson> ExecuteAsync(RequestRegisterBillingJson request);

}
