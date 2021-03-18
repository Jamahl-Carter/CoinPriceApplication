using CoinPrice.Contract;
using Microsoft.Extensions.Caching.Memory;

namespace CoinPrice.Data.Repository.Implementation
{
    internal class UserRepository : IUserRepository
    {
        private const string USER_PREFERENCE_KEY = "USER_PREFERENCE";

        private readonly IMemoryCache _memoryCache;

        public UserRepository(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        
        public CoinType FetchPreferredCoin()
        {
            return _memoryCache.TryGetValue(USER_PREFERENCE_KEY, out CoinType coinType) 
                ? coinType // use if configured
                : CoinType.BTC; // default to BTC
        }

        public void SetPreferredCoin(CoinType coinType) => _memoryCache.Set(USER_PREFERENCE_KEY, coinType);
    }
}
