using AutoMapper;
using BarberFlow.Communication.Requests;
using BarberFlow.Communication.Responses;
using BarberFlow.Domain.Repositories;
using BarberFlow.Domain.Repositories.Billings;
using BarberFlow.Exception.ExceptionBase;

namespace BarberFlow.Application.UseCases.Billings.Register;

public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IBillingsWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterBillingUseCase(IBillingsWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisterBillingJson> ExecuteAsync(RequestRegisterBillingJson request)
    {
        Validate(request);
        var entity = _mapper.Map<Domain.Entities.Billing>(request);

        await _repository.AddAsync(entity);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterBillingJson>(entity);

    }

    private static void Validate(RequestRegisterBillingJson request)
    {
        var validator = new BillingValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
