namespace CurrencyExchange.Core.Common
{
    public static class Constants
    {
        public static readonly string[] SellCurrencies = ["AUD", "USD", "EUR"];
        public static readonly string[] BuyCurrencies = ["USD", "INR", "PHP"];
        public static readonly string RegExForName = @"^[A-Za-z\s-']*$";
        public static readonly string RegExForTransferReason = @"^[A-Za-z\s]*$";
        public static readonly string RegExForBankName = @"^[A-Za-z\s]*$";
        public static readonly string RegExDigits = @"^[0-9]*$";
    }
}
