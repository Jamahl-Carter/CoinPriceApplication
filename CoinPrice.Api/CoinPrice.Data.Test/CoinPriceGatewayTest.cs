using System.Threading.Tasks;
using AutoFixture;
using CoinPrice.Contract;
using CoinPrice.Data.Client;
using CoinPrice.Data.Dto;
using CoinPrice.Data.Gateway.Implementation;
using Moq;
using Xunit;

namespace CoinPrice.Data.Test
{
    public class CoinPriceGatewayTest
    {
        [Fact]
        public async Task FetchCoinPriceAsync_Success()
        {
            // Arrange
            var fixture = new Fixture();
            var inputCoinType = CoinType.ETH;
            var expectedResponse = fixture.Build<PriceResponse>().Create();
            var mockCointreeClient = new Mock<ICointreeClient>();

            mockCointreeClient.Setup(client => client.GetPriceAsync(inputCoinType)).ReturnsAsync(expectedResponse);

            fixture.Register(() => mockCointreeClient.Object);

            var target = fixture.Create<CoinPriceGateway>();

            // Act
            PriceResponse actual = await target.FetchCoinPriceAsync(inputCoinType);

            // Assert
            Assert.Equal(expectedResponse, actual); // expect same instance to return

            mockCointreeClient.Verify(client => client.GetPriceAsync(inputCoinType), Times.Once);
            mockCointreeClient.VerifyNoOtherCalls();
        }
    }
}
