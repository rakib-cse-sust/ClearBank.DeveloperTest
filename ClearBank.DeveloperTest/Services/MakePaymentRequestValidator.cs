using ClearBank.DeveloperTest.Types;
using FluentValidation;

namespace ClearBank.DeveloperTest.Services
{
    public class MakePaymentRequestValidator : AbstractValidator<MakePaymentRequest>
    {
        public MakePaymentRequestValidator()
        {
            RuleFor(p => p.DebtorAccountNumber).NotEmpty().WithMessage("Account Number can't be empty");
            RuleFor(p => p.Amount).NotEmpty().WithMessage("Requested amount can't be empty or 0");
        }
    }
}
