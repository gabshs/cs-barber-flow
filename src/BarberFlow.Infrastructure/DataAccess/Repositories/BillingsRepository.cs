using BarberFlow.Domain.Entities;
using BarberFlow.Domain.Repositories.Billings;
using Microsoft.EntityFrameworkCore;

namespace BarberFlow.Infrastructure.DataAccess.Repositories;

internal class BillingsRepository : IBillingsReadOnlyRepository, IBillingsWriteOnlyRepository, IBillingUpdateOnlyRepository
{
    private readonly BarberFlowDbContext _dbContext;

    public BillingsRepository(BarberFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Billing?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Billings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Billing>> GetBillingsByMonthAsync(DateOnly date)
    {
        var startDate = new DateOnly(year: date.Year, month: date.Month, day: 1);
        var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
        var endDate = new DateOnly(year: date.Year, month: date.Month, day: daysInMonth);
        return await _dbContext.Billings
            .AsNoTracking()
            .Where(b => b.Date >= startDate && b.Date <= endDate)
            .OrderBy(b => b.Date)
            .ThenBy(b => b.ServiceName)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Billing> Items, int TotalCount)> GetAllPaginatedAsync(int pageNumber, int pageSize, string? orderBy = null, bool? isDescending = null, string? filter = null)
    {
        IQueryable<Billing> query = _dbContext.Billings;

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(b => b.BarberName.Contains(filter) ||
                                     b.ClientName.Contains(filter) ||
                                     b.ServiceName.Contains(filter));
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            query = orderBy switch
            {
                "date" => query.OrderBy(b => b.Date),
                "barber" => query.OrderBy(b => b.BarberName),
                "client" => query.OrderBy(b => b.ClientName),
                "service" => query.OrderBy(b => b.ServiceName),
                _ => query.OrderBy(b => b.Date)
            };
        }

        if (isDescending == true)
        {
            query = query.OrderByDescending(b => b.Date);
        }

        var totalCount = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return (items, totalCount);
    }

    public async Task AddAsync(Billing billing)
    {
        try
        {
            await _dbContext.Billings.AddAsync(billing);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding billing: {ex.Message}");
        }

    }

    public async Task UpdateAsync(Billing billing)
    {
        _dbContext.Billings.Update(billing);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var billing = await _dbContext.Billings.FindAsync(id);
        if (billing != null)
        {
            _dbContext.Billings.Remove(billing);
            await _dbContext.SaveChangesAsync();
        }
    }

}
