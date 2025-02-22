using CurrencyExchange.Core.Interfaces.Services;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CurrencyExchange.Application.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly Dictionary<string, decimal> _exchangeRates = new();
        private readonly HttpClient _httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class APIResult
        {
            public decimal conversion_rate { get;set; }
        }

        public async Task<decimal?> GetExchangeRates(string sellCurrrency, string buyCurrrency)
        {
            string key = $"{sellCurrrency}-{buyCurrrency}";
            try
            {
                if (_exchangeRates.ContainsKey(key))
                {
                    return _exchangeRates[key];
                }

                var url = $"https://v6.exchangerate-api.com/v6/0c489876513ad33b67e61a79/pair/{sellCurrrency}/{buyCurrrency}";

                //using (var webClient = new System.Net.WebClient())
                //{
                //    var json = webClient.DownloadString(url);
                //    var response = JsonConvert.DeserializeObject<APIResult>(json);
                //        if (response != null)
                //    {
                //        _exchangeRates[key] = (decimal)response?.conversion_rate;
                //        return _exchangeRates[key];
                //    }
                //}
                ////            https://v6.exchangerate-api.com/v6/
                var response = await _httpClient.GetFromJsonAsync<APIResult>(url);

                if (response != null)
                {
                    _exchangeRates[key] = (decimal)response?.conversion_rate;
                    return _exchangeRates[key];
                }
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
