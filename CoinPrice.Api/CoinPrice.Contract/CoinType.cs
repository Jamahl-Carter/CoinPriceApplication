using System.Text.Json.Serialization;

namespace CoinPrice.Contract
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // display ENUM as string
    public enum CoinType
    {
        BTC = 0,
        ETH = 1,
        XRP = 2
    }
}
