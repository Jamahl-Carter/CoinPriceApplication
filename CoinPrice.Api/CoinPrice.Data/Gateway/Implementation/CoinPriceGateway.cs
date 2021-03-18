using System.Threading.Tasks;
using CoinPrice.Contract;
using CoinPrice.Data.Client;
using CoinPrice.Data.Dto;

namespace CoinPrice.Data.Gateway.Implementation
{
    internal class CoinPriceGateway : ICoinPriceGateway
    {
        private readonly ICointreeClient _cointreeClient;

        public CoinPriceGateway(ICointreeClient cointreeClient) => _cointreeClient = cointreeClient;

        public Task<PriceResponse> FetchCoinPriceAsync(CoinType coinType) => _cointreeClient.GetPriceAsync(coinType);
    }
}
