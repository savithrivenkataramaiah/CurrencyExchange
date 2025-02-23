using CurrencyExchange.Core.Common;
using CurrencyExchange.DTO;
using FluentValidation;

namespace CurrencyExchange.Application.Validators
{
    public class PayerValidator : AbstractValidator<Payer>
    {
        public PayerValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Payer Id cannot be empty.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Payer Name cannot be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).MinimumLength(2).WithMessage("Payer Name must have minimum of 2 characters")
                    .MaximumLength(40).WithMessage("Payer Name can have a maximum of 40 characters")
                    .Matches(Constants.RegExForName).WithMessage("Invalid Characters for Name. Allowed characters are: A-Za-z'- and a space");
                });
            RuleFor(x => x.TransferReason).NotEmpty().WithMessage("Transfer Reason cannot be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.TransferReason).MinimumLength(3).WithMessage("Transfer Reason must have minimum of 3 characters")
                    .MaximumLength(30).WithMessage("Transfer Reason can have a maximum of 30 characters")
                    .Matches(Constants.RegExForTransferReason).WithMessage("Invalid Characters for Transfer Reason. Allowed characters are: A-Za-z and a space");
                });
        }
    }
}
