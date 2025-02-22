using CurrencyExchange.DTO;
using FluentValidation;

namespace CurrencyExchange.Application.Validators
{
    public class TransferValidator : AbstractValidator<TransferRequest>
    {
        public TransferValidator() {
            RuleFor(x => x.QuoteId).NotEmpty().WithMessage("QuoteId cannot be empty.");
            RuleFor(x => x.Payer).NotNull().WithMessage("Payer details cannot be empty.").SetValidator(new PayerValidator());
            RuleFor(x => x.Recipient).NotNull().WithMessage("Recipient details cannot be empty.").SetValidator(new RecipientValidator());
        }
    }
}
