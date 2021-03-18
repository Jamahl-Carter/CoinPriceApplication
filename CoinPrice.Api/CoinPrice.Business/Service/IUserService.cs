using System.Threading.Tasks;
using CoinPrice.Contract;

namespace CoinPrice.Business.Service
{
    public interface IUserService
    {
        Task SetPreferredCoinAsync(CoinType coinType);
    }
}
