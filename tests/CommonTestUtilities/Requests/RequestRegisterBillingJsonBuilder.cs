using System;
using BarberFlow.Communication.Enums;
using BarberFlow.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;

public abstract class RequestRegisterBillingJsonBuilder
{
    public static RequestRegisterBillingJson Build()
    {
        var faker = new Faker();

        return new RequestRegisterBillingJson
        {
            BarberName = faker.Name.FullName(),
            Date = DateOnly.FromDateTime(faker.Date.Past()),
            ClientName = faker.Name.FullName(),
            ServiceName = faker.Commerce.ProductName(),
            Amount = faker.Finance.Amount(20, 500),
            PaymentMethod = faker.PickRandom<PaymentMethod>(),
            Status = faker.PickRandom<BillingStatus>()
        };
    }

}
