using System;
using AutoMapper;
using BarberFlow.Communication.Responses;
using BarberFlow.Domain.Repositories.Billings;
using BarberFlow.Exception.ExceptionBase;

namespace BarberFlow.Application.UseCases.Billings.GetById;

public class GetBillingByIdUseCase : IGetBillingByIdUseCase
{
    private readonly IBillingsReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetBillingByIdUseCase(IBillingsReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseBillingJson> ExecuteAsync(Guid billingId)
    {
        var billing = await _repository.GetByIdAsync(billingId) ?? throw new NotFoundException("Billing not found");
        var billingResponse = _mapper.Map<ResponseBillingJson>(billing);

        return billingResponse;

    }
}
