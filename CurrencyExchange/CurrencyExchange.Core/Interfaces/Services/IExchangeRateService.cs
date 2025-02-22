namespace CurrencyExchange.Core.Interfaces.Services
{
    public interface IExchangeRateService
    {
        Task<decimal?> GetExchangeRates(string sellCurrrency, string buyCurrrency);
    }
}
