using CoinPrice.Contract;

namespace CoinPrice.Data.Repository
{
    public interface IUserRepository
    {
        CoinType FetchPreferredCoin();
        void SetPreferredCoin(CoinType coinType);
    }
}
