using System.Threading.Tasks;
using AutoFixture;
using CoinPrice.Business.Service.Implementation;
using CoinPrice.Contract;
using CoinPrice.Contract.Message;
using CoinPrice.Data.Dto;
using CoinPrice.Data.Gateway;
using CoinPrice.Data.Repository;
using Moq;
using Xunit;

namespace CoinPrice.Business.Test
{
    public class PriceCheckServiceTest
    {
        [Fact]
        public async Task FetchCoinPriceAsync_Success()
        {
            // Arrange
            var fixture = new Fixture();

            var expectedPreferredCoin = CoinType.ETH;
            var expectedResponse = fixture.Build<PriceResponse>().Create();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockCoinPriceGateway = new Mock<ICoinPriceGateway>();

            mockUserRepository.Setup(repo => repo.FetchPreferredCoin()).Returns(expectedPreferredCoin);
            mockCoinPriceGateway.Setup(gateway => gateway.FetchCoinPriceAsync(expectedPreferredCoin))
                .ReturnsAsync(expectedResponse);

            fixture.Register(() => mockUserRepository.Object);
            fixture.Register(() => mockCoinPriceGateway.Object);

            var target = fixture.Create<PriceCheckService>();

            // Act
            CoinPriceResponse actual = await target.FetchCoinPriceAsync();

            // Assert
            mockUserRepository.Verify(repo => repo.FetchPreferredCoin(), Times.Once);
            mockCoinPriceGateway.Verify(gateway => gateway.FetchCoinPriceAsync(expectedPreferredCoin), Times.Once);
            mockUserRepository.VerifyNoOtherCalls();
            mockCoinPriceGateway.VerifyNoOtherCalls();

            Assert.Equal(expectedResponse.Rate, actual.Rate);
            Assert.Equal(expectedResponse.Bid, actual.Bid);
            Assert.Equal(expectedResponse.Ask, actual.Ask);
        }
    }
}
