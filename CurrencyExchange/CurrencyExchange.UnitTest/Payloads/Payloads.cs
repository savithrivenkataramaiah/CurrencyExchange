using CurrencyExchange.Core.Entities;
using CurrencyExchange.DTO;

namespace CurrencyExchange.UnitTest.Payloads
{
    internal static class Payloads
    {
        public static Transfer GetTransferObject()
        {
            return new Transfer
            {
                QuoteId = Guid.NewGuid(),
                TransferId = Guid.NewGuid(),
                EstimatedDeliveryDate = DateTime.Now,
                Payer = new Core.Entities.Payer
                {
                    Id = Guid.NewGuid(),
                    Name = "John Smith",
                    TransferReason = "Self"
                },
                Recipient = new Core.Entities.Recipient
                {
                    Name = "Jacob Wood",
                    AccountNumber = "12345678",
                    BankCode = "12345",
                    BankName = "Bank of Australia"
                }
            };
        }
        public static Transfer GetTransferObject(TransferRequest request)
        {
            return new Transfer
            {
                QuoteId = request.QuoteId,
                TransferId = Guid.NewGuid(),
                EstimatedDeliveryDate = DateTime.Now,
                Payer = request.Payer ==null ? null: new Core.Entities.Payer
                {
                    Id = request.Payer.Id,
                    Name = request.Payer.Name,
                    TransferReason = request.Payer.TransferReason
                },
                Recipient = request.Recipient == null ? null : new Core.Entities.Recipient
                {
                    Name=request.Recipient.Name,
                   AccountNumber=request.Recipient.AccountNumber,
                   BankCode=request.Recipient.BankCode, 
                   BankName=request.Recipient.BankName
                }
            };
        }

        public static TransferRequest GetTransferRequest()
        {
            return new TransferRequest
            {
                QuoteId = Guid.NewGuid(),
                Payer = new DTO.Payer
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    TransferReason = "Invoice"
                },
                Recipient = new DTO.Recipient
                {
                    Name = "Clint Wood",
                    AccountNumber = "1234567890",
                    BankCode = "001341",
                    BankName = "Bank of World"
                }
            };
        }

        public static Quote GetQuote(QuoteRequest request, decimal exchangeRate)
        {
            return new Quote
            {
                QuoteId = Guid.NewGuid(),
                Amount = request.Amount,
                BuyCurrency = request.BuyCurrency,
                SellCurrency = request.SellCurrency,
                ConvertedAmount = exchangeRate * request.Amount,
                InverseOfxRate = 1 / exchangeRate,
                OfxRate = exchangeRate
            };
        }
    }
}
