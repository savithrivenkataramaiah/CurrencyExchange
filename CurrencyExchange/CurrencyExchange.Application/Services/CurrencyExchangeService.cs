using CurrencyExchange.Application.Validators;
using CurrencyExchange.Core.Entities;
using CurrencyExchange.Core.Interfaces.Repositories;
using CurrencyExchange.Core.Interfaces.Services;
using CurrencyExchange.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly IExchangeRateService _exchangeRateService;

        public CurrencyExchangeService(IQuoteRepository quoteRepository, IExchangeRateService exchangeRateService, ITransferRepository transferRepository)
        {
            _quoteRepository = quoteRepository;
            _exchangeRateService = exchangeRateService;
            _transferRepository = transferRepository;
        }

        public async Task<QuoteResponse> CreateQuoteAsync(QuoteRequest quoteRequest)
        {
            QuoteResponse quoteResponse = new QuoteResponse();
            try
            {
                var validationResult = new QuoteValidator().Validate(quoteRequest);
                if (!validationResult.IsValid)
                    throw new ArgumentException(string.Join("\r\n", validationResult.Errors.Select(e => e.ErrorMessage)));

                var exchangeRate = await _exchangeRateService.GetExchangeRates(quoteRequest.SellCurrency, quoteRequest.BuyCurrency);

                if (exchangeRate == null)
                    throw new ArgumentException("Conversion rate was not found");

                var quote = _quoteRepository.CreateQuote(GetQuoteFromRequest(quoteRequest, exchangeRate.Value));
                quoteResponse = GetQuoteResponse(quote);
            }
            catch (Exception ex) {  }
            return quoteResponse;
        }

        public QuoteResponse GetQuoteByQuoteId(Guid quoteId)
        {
            var quote = _quoteRepository.GetQuoteByQuoteId(quoteId);
            if (quote == null) return null;

            return GetQuoteResponse(quote);
        }

        public TransferResponse CreateTransfer(TransferRequest transferRequest)
        {
            TransferResponse transferResponse = null;
            try
            {
                var validationResult = new TransferValidator().Validate(transferRequest);
                if (!validationResult.IsValid)
                    throw new ArgumentException(string.Join("\r\n", validationResult.Errors.Select(e => e.ErrorMessage)));

                var quote = _quoteRepository.GetQuoteByQuoteId(transferRequest.QuoteId);
                if (quote == null)
                    throw new ArgumentException("Incorrect Quote Id was provided to create a transfer.");

                var transfer = _transferRepository.CreateTransfer(GetTransferFromRequest(transferRequest));
                transferResponse = GetTransferResponse(transfer);
            }
            catch (Exception ex) { throw ex; }
            return transferResponse;
        }

        public TransferResponse GetTransferByTransferId(Guid transferId)
        {
            var transfer = _transferRepository.GetTransferByTransferId(transferId);
            if (transfer == null) return null;

            return GetTransferResponse(transfer);
        }

        private Quote GetQuoteFromRequest(QuoteRequest quoteRequest, decimal exchangeRate)
        {
            return new Quote
            {
                QuoteId = Guid.NewGuid(),
                SellCurrency = quoteRequest.SellCurrency,
                BuyCurrency = quoteRequest.BuyCurrency,
                Amount = quoteRequest.Amount,
                OfxRate = exchangeRate,
                InverseOfxRate = 1 / exchangeRate,
                ConvertedAmount = quoteRequest.Amount * exchangeRate
            };
        }

        private QuoteResponse GetQuoteResponse(Quote quote) {
            return new QuoteResponse
            {
                QuoteId = quote.QuoteId,
                OfxRate = Math.Round(quote.OfxRate, 6),
                InverseOfxRate = Math.Round(quote.InverseOfxRate, 5),
                ConvertedAmount = Math.Round(quote.ConvertedAmount, 2),
            };
        }

        private Transfer GetTransferFromRequest(TransferRequest transferRequest)
        {
            return new Transfer
            {
                TransferId = Guid.NewGuid(),
                QuoteId = transferRequest.QuoteId,
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(1),
                Status = Core.Enums.TransferStatus.Created,
                Payer = new Core.Entities.Payer
                {
                    Id = Guid.NewGuid(),
                    Name = transferRequest.Payer.Name,
                    TransferReason = transferRequest.Payer.TransferReason,
                },
                Recipient = new Core.Entities.Recipient
                {
                    Name = transferRequest.Recipient.Name,
                    AccountNumber = transferRequest.Recipient.AccountNumber,
                    BankName = transferRequest.Recipient.BankName,
                    BankCode = transferRequest.Recipient.BankCode
                }
            };
        }

        private TransferResponse GetTransferResponse(Transfer transfer)
        {
            return new TransferResponse
            {
                TransferId = transfer.TransferId,
                Status = transfer.Status.ToString(),
                TransferDetails = new TransferRequest
                {
                    QuoteId = transfer.QuoteId,
                    Payer = new DTO.Payer
                    {
                        Id = transfer.Payer.Id,
                        Name = transfer.Payer.Name,
                        TransferReason = transfer.Payer.TransferReason
                    },
                    Recipient = new DTO.Recipient
                    {
                        Name = transfer.Recipient.Name,
                        AccountNumber= transfer.Recipient.AccountNumber,
                        BankName= transfer.Recipient.BankName,
                        BankCode = transfer.Recipient.BankCode  
                    }

                }
            };
        }
    }
}
