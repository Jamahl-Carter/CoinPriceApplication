using System.Threading.Tasks;
using AutoFixture;
using CoinPrice.Business.Service.Implementation;
using CoinPrice.Contract;
using CoinPrice.Data.Repository;
using Moq;
using Xunit;

namespace CoinPrice.Business.Test
{
    public class UserServiceTest
    {
        [Fact]
        public async Task SetPreferredCoinAsync_Success()
        {
            // Arrange
            var inputCoinType = CoinType.BTC;
            var mockUserRepo = new Mock<IUserRepository>();

            var fixture = new Fixture();
            fixture.Register(() => mockUserRepo.Object);

            var target = fixture.Create<UserService>();

            // Act
            await target.SetPreferredCoinAsync(inputCoinType);

            // Assert
            mockUserRepo.Verify(repo => repo.SetPreferredCoin(inputCoinType), Times.Once);
            mockUserRepo.VerifyNoOtherCalls();
        }
    }
}
