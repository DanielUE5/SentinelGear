namespace SentinelGear.Helpers
{
    public static class CurrencyConverter
    {
        private const decimal BgnToEurRate = 1.95583m;

        public static decimal ConvertBgnToEur(decimal priceInBgn)
        {
            return Math.Round(priceInBgn / BgnToEurRate, 2);
        }
    }
}
