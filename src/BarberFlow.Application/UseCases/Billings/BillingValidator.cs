using System;
using BarberFlow.Communication.Requests;
using FluentValidation;

namespace BarberFlow.Application.UseCases.Billings;

public class BillingValidator : AbstractValidator<RequestRegisterBillingJson>
{
    public BillingValidator()
    {
        RuleFor(billing => billing.BarberName)
            .NotEmpty().WithMessage("The BarberName field is required.")
            .MaximumLength(80).WithMessage("The BarberName field must not exceed 80 characters.");
        RuleFor(billing => billing.ClientName)
            .NotEmpty().WithMessage("The ClientName field is required.")
            .MaximumLength(120).WithMessage("The ClientName field must not exceed 120 characters.");
        RuleFor(billing => billing.ServiceName)
            .NotEmpty().WithMessage("The ServiceName field is required.")
            .MaximumLength(100).WithMessage("The ServiceName field must not exceed 100 characters.");
        RuleFor(billing => billing.Amount)
            .GreaterThan(0).WithMessage("The Amount field must be greater than zero.");
        RuleFor(billing => billing.PaymentMethod).IsInEnum()
            .WithMessage("The PaymentMethod field must be a valid payment method.");
        RuleFor(billing => billing.Status).IsInEnum()
            .WithMessage("The Status field must be a valid billing status.");
    }
}
