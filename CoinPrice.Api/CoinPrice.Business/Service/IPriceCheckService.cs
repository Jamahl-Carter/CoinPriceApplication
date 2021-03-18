using System.Threading.Tasks;
using CoinPrice.Contract.Message;

namespace CoinPrice.Business.Service
{
    public interface IPriceCheckService
    {
        Task<CoinPriceResponse> FetchCoinPriceAsync();
    }
}
