namespace CoinPrice.Contract.Message
{
    public class CoinPriceResponse
    {
        public CoinType CoinType { get; set; }
        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
        public decimal Rate { get; set; }
    }
}
