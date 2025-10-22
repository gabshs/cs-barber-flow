using BarberFlow.Application.UseCases.Billings;
using BarberFlow.Communication.Enums;
using CommonTestUtilities.Requests;

namespace Validators.Tests.Billings.Register
{
    public class RegisterBillingValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();

            var result = validator.Validate(request);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ErrorServiceNameEmpty()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.ServiceName = string.Empty;

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "ServiceName");
        }

        [Fact]
        public void ErrorClientNameEmpty()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.ClientName = string.Empty;

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "ClientName");
        }

        [Fact]
        public void ErrorBarberNameEmpty()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.BarberName = string.Empty;

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "BarberName");
        }

        [Fact]
        public void ErrorAmountZero()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.Amount = 0;

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Amount");
        }

        [Fact]
        public void ErrorPaymentMethodInvalid()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.PaymentMethod = (PaymentMethod)999;

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "PaymentMethod");
        }

        [Fact]
        public void ErrorStatusInvalid()
        {
            var validator = new BillingValidator();
            var request = RequestRegisterBillingJsonBuilder.Build();
            request.Status = (BillingStatus)999;

            var result = validator.Validate(request);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Status");
        }
    }
}