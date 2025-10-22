using System;
using BarberFlow.Communication.Requests;
using BarberFlow.Communication.Responses;

namespace BarberFlow.Application.UseCases.Billings.ListAll;

public interface IListAllBillingsUseCase
{
    Task<ResponsePaginatedJson<ResponseListBillingsJson>> ExecuteAsync(RequestListPaginatedBillingsJson filters);

}
