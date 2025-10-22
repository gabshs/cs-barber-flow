using System;
using AutoMapper;
using BarberFlow.Communication.Requests;
using BarberFlow.Domain.Repositories;
using BarberFlow.Domain.Repositories.Billings;
using BarberFlow.Exception.ExceptionBase;

namespace BarberFlow.Application.UseCases.Billings.Update;

public class UpdateBillingUseCase : IUpdateBillingUseCase
{
    private readonly IBillingUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBillingUseCase(IBillingUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(Guid id, RequestRegisterBillingJson request)
    {
        Validate(request);

        var billing = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Billing not found.");

        _mapper.Map(request, billing);

        await _repository.UpdateAsync(billing);
        await _unitOfWork.Commit();
    }

    private static void Validate(RequestRegisterBillingJson body)
    {
        var validator = new BillingValidator();
        var result = validator.Validate(body);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }

}
