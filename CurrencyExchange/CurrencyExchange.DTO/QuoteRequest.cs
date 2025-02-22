namespace CurrencyExchange.DTO
{
    public class QuoteRequest
    {
        public string SellCurrency {  get; set; }
        public string BuyCurrency { get; set; }
        public decimal Amount { get; set; } 
    }
}
