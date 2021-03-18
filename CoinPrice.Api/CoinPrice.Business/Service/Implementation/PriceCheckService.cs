using System.Threading.Tasks;
using CoinPrice.Contract;
using CoinPrice.Contract.Message;
using CoinPrice.Data.Dto;
using CoinPrice.Data.Gateway;
using CoinPrice.Data.Repository;

namespace CoinPrice.Business.Service.Implementation
{
    internal class PriceCheckService : IPriceCheckService
    {
        private readonly ICoinPriceGateway _coinPriceGateway;
        private readonly IUserRepository _userRepository;

        public PriceCheckService(ICoinPriceGateway coinPriceGateway, IUserRepository userRepository)
        {
            _coinPriceGateway = coinPriceGateway;
            _userRepository = userRepository;
        }

        public async Task<CoinPriceResponse> FetchCoinPriceAsync()
        {
            // Fetch preferred coin from in-memory cache
            CoinType preferredCoin = _userRepository.FetchPreferredCoin();

            // Fetch price data from coin
            PriceResponse response = await _coinPriceGateway.FetchCoinPriceAsync(preferredCoin).ConfigureAwait(false);

            // Map and return response
            return new CoinPriceResponse
            {
                CoinType = preferredCoin,
                Rate = response.Rate,
                Bid = response.Bid,
                Ask = response.Ask
            };
        }
    }
}
