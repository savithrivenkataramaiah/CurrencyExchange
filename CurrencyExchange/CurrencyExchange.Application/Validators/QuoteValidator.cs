using CurrencyExchange.Core.Common;
using CurrencyExchange.DTO;
using FluentValidation;

namespace CurrencyExchange.Application.Validators
{
    public class QuoteValidator : AbstractValidator<QuoteRequest>
    {
        public QuoteValidator()
        {
            RuleFor(x => x.SellCurrency).NotEmpty().WithMessage("Sell currency value cannot be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.SellCurrency).Must(x => Constants.SellCurrencies.Contains(x))
                    .WithMessage("Invalid entry, Allowed values for Sell currency are: " + string.Join(", ", Constants.SellCurrencies));
                });
            RuleFor(x => x.BuyCurrency).NotEmpty().WithMessage("Buy currency value cannot be empty.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.BuyCurrency).Must(x => Constants.BuyCurrencies.Contains(x))
                    .WithMessage("Invalid entry, Allowed values for Buy currency are: " + string.Join(", ", Constants.BuyCurrencies))
                    .DependentRules(() =>
                    {
                        RuleFor(x => x.BuyCurrency).NotEqual(x=>x.SellCurrency)
                        .WithMessage("Value for Buy currency cannot be the same as Sell Currency.");
                    });
                });
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be entered and should be greater than zero")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Amount).LessThanOrEqualTo(20000).WithMessage("Amount cannot be greater than 20000");
                });
        }
    }
}
