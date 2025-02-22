namespace CurrencyExchange.DTO
{
    public class TransferRequest
    {
        public Guid QuoteId { get; set; }
        public Payer Payer { get; set; }
        public Recipient Recipient { get; set; }
    }

    public class Payer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TransferReason { get; set; }
    }

    public class Recipient
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
    }
}
