namespace BarberFlow.Application.UseCases.Billings.Delete;

public interface IDeleteBillingUseCase
{
    Task ExecuteAsync(Guid id);
}
