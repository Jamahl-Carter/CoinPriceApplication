using System.Threading.Tasks;
using CoinPrice.Contract;
using CoinPrice.Data.Dto;

namespace CoinPrice.Data.Gateway
{
    public interface ICoinPriceGateway
    {
        Task<PriceResponse> FetchCoinPriceAsync(CoinType coinType);
    }
}
