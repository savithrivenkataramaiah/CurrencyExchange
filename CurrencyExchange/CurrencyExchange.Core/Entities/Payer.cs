namespace CurrencyExchange.Core.Entities
{
    public class Payer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TransferReason { get; set; }
    }
}
