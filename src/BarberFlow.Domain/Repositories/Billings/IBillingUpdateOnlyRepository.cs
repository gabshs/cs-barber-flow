using System;
using BarberFlow.Domain.Entities;

namespace BarberFlow.Domain.Repositories.Billings;

public interface IBillingUpdateOnlyRepository
{
    Task UpdateAsync(Billing billing);
    Task<Billing> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);

}
