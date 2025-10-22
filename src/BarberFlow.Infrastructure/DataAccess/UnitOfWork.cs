using BarberFlow.Domain.Repositories;

namespace BarberFlow.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly BarberFlowDbContext _dbContext;

    public UnitOfWork(BarberFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}