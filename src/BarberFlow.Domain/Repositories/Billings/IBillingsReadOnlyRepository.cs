using BarberFlow.Domain.Entities;

namespace BarberFlow.Domain.Repositories.Billings;

public interface IBillingsReadOnlyRepository
{
    Task<Billing?> GetByIdAsync(Guid id);
    Task<(IEnumerable<Billing> Items, int TotalCount)> GetAllPaginatedAsync(int pageNumber, int pageSize, string? orderBy = null, bool? isDescending = null, string? filter = null);
    Task<IEnumerable<Billing>> GetBillingsByMonthAsync(DateOnly month);

}
