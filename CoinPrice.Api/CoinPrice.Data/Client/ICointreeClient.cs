using System.Threading.Tasks;
using CoinPrice.Contract;
using CoinPrice.Data.Dto;

namespace CoinPrice.Data.Client
{
    public interface ICointreeClient
    {
        Task<PriceResponse> GetPriceAsync(CoinType coinType);
    }
}
