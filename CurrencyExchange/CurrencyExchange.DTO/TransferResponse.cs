namespace CurrencyExchange.DTO
{
    public class TransferResponse
    {
        public Guid TransferId { get; set; }
        public string Status { get; set; }
        public TransferRequest TransferDetails { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }

    }
}
