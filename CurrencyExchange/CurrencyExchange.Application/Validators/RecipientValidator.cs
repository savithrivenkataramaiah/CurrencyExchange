using CurrencyExchange.Core.Common;
using CurrencyExchange.DTO;
using FluentValidation;

namespace CurrencyExchange.Application.Validators
{
    public  class RecipientValidator : AbstractValidator<Recipient>
    {
        public RecipientValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Recipient Name cannot be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name).MinimumLength(2).WithMessage("Recipient Name must have minimum of 2 characters")
                    .MaximumLength(40).WithMessage("Recipient Name can have a maximum of 40 characters")
                    .Matches(Constants.RegExForName).WithMessage("Invalid Characters for Name. Allowed characters are: A-Za-z'- and a space");
                });
            RuleFor(x => x.AccountNumber).NotEmpty().WithMessage("Account Number cannot be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.AccountNumber).MinimumLength(6).WithMessage("AccountNumber must have minimum of 6 characters")
                    .MaximumLength(12).WithMessage("Account Number can have a maximum of 12 characters")
                    .Matches(Constants.RegExDigits).WithMessage("Only number 0-9 are allowed for Account Number");
                });
            RuleFor(x => x.BankName).NotEmpty().WithMessage("Bank Name cannot be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.BankName).MinimumLength(3).WithMessage("Bank Name must have minimum of 3 characters")
                    .MaximumLength(50).WithMessage("Bank Name can have a maximum of 50 characters")
                    .Matches(Constants.RegExForBankName).WithMessage("Invalid Characters for Bank Name. Allowed characters are: A-Za-z and a space");
                });
            RuleFor(x => x.BankCode).NotEmpty().WithMessage("Bank Code cannot be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.BankCode).Length(6).WithMessage("Bank Code must have 6 characters")
                    .Matches(Constants.RegExDigits).WithMessage("Only number 0-9 are allowed for Bank Code");
                });
        }
    }
}
