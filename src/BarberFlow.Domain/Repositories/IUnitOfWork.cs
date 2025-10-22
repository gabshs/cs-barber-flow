namespace BarberFlow.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}