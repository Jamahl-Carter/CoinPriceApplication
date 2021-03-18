using System.Threading.Tasks;
using CoinPrice.Contract;
using CoinPrice.Data.Repository;

namespace CoinPrice.Business.Service.Implementation
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public Task SetPreferredCoinAsync(CoinType coinType)
        {
            _userRepository.SetPreferredCoin(coinType);

            return Task.CompletedTask;
        }
    }
}
