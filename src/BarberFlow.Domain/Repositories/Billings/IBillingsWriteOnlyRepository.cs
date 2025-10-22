using System;
using BarberFlow.Domain.Entities;

namespace BarberFlow.Domain.Repositories.Billings;

public interface IBillingsWriteOnlyRepository
{
    Task AddAsync(Billing billing);

}
