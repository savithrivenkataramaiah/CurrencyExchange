namespace CurrencyExchange.Core.Entities
{
    public class Quote
    {
        public Guid QuoteId { get; set; }
        public string SellCurrency { get; set; }
        public string BuyCurrency { get; set; }
        public decimal Amount { get; set; }
        public decimal OfxRate { get; set; }
        public decimal InverseOfxRate { get; set; }
        public decimal ConvertedAmount { get; set; }
    }
}
