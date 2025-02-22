using CurrencyExchange.Core.Entities;
using CurrencyExchange.Core.Interfaces.Repositories;

namespace CurrencyExchange.Infrastructure
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly List<Quote> _quotes = new List<Quote>();
        public Quote CreateQuote(Quote quote)
        {
            _quotes.Add(quote);
            return quote;
        }

        public Quote GetQuoteByQuoteId(Guid quoteId)
        {
            return _quotes.FirstOrDefault(quote => quote.QuoteId == quoteId);
        }
    }
}
