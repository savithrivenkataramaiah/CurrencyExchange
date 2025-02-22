using CurrencyExchange.Core.Enums;

namespace CurrencyExchange.Core.Entities
{
    public class Transfer
    {
        public Guid TransferId { get; set; }
        public Guid QuoteId { get; set; }
        public TransferStatus Status { get; set; }
        public Payer Payer { get; set; }
        public Recipient Recipient { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
    }
}
