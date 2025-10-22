using System;
using AutoMapper;
using BarberFlow.Communication.Requests;
using BarberFlow.Communication.Responses;
using BarberFlow.Domain.Repositories.Billings;

namespace BarberFlow.Application.UseCases.Billings.ListAll;

public class ListAllBillingsUseCase : IListAllBillingsUseCase
{
    private readonly IBillingsReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public ListAllBillingsUseCase(IBillingsReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponsePaginatedJson<ResponseListBillingsJson>> ExecuteAsync(RequestListPaginatedBillingsJson filters)
    {
        var (billings, totalCount) = await _repository.GetAllPaginatedAsync(filters.PageNumber ?? 1, filters.PageSize ?? 10, filters.SearchTerm);
        var billingsResponse = _mapper.Map<IEnumerable<ResponseListBillingsJson>>(billings);

        var response = new ResponsePaginatedJson<ResponseListBillingsJson>
        {
            Items = billingsResponse,
            TotalCount = totalCount,
            PageNumber = filters.PageNumber ?? 1,
            PageSize = filters.PageSize ?? 10,
            OrderBy = filters.OrderBy,
            IsDescending = filters.IsDescending
        };

        return response;

    }
}
