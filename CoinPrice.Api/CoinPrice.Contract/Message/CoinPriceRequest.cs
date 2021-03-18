using System.ComponentModel.DataAnnotations;

namespace CoinPrice.Contract.Message
{
    public class CoinPriceRequest
    {
        [EnumDataType(typeof(CoinType))]
        public CoinType CoinType { get; set; } = CoinType.BTC; // default to BTC
    }
}
