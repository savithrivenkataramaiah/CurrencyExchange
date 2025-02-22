using CurrencyExchange.Core.Entities;
using CurrencyExchange.DTO;

namespace CurrencyExchange.Core.Interfaces.Services
{
    public interface ICurrencyExchangeService
    {
        Task<QuoteResponse> CreateQuoteAsync(QuoteRequest quoteRequest);
        QuoteResponse GetQuoteByQuoteId(Guid quoteId);
        TransferResponse CreateTransfer(TransferRequest transfer);
        TransferResponse GetTransferByTransferId(Guid quoteId);
    }
}
