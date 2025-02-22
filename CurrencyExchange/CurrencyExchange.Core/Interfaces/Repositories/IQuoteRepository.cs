using CurrencyExchange.Core.Entities;

namespace CurrencyExchange.Core.Interfaces.Repositories
{
    public interface IQuoteRepository
    {
        Quote CreateQuote(Quote quote);
        Quote GetQuoteByQuoteId(Guid quoteId);
    }
}
