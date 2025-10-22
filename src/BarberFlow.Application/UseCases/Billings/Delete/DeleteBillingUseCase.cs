using BarberFlow.Domain.Repositories;
using BarberFlow.Domain.Repositories.Billings;
using BarberFlow.Exception.ExceptionBase;

namespace BarberFlow.Application.UseCases.Billings.Delete;

public class DeleteBillingUseCase : IDeleteBillingUseCase
{
    private readonly IBillingUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBillingUseCase(IBillingUpdateOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var billing = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Billing not found.");

        await _repository.DeleteAsync(id);
        await _unitOfWork.Commit();
    }
}
