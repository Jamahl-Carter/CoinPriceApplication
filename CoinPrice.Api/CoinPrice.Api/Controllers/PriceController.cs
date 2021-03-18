using System.Threading.Tasks;
using CoinPrice.Business.Service;
using CoinPrice.Contract.Message;
using Microsoft.AspNetCore.Mvc;

namespace CoinPrice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceCheckService _priceCheckService;

        public PriceController(IPriceCheckService priceCheckService) => _priceCheckService = priceCheckService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            CoinPriceResponse result = await _priceCheckService.FetchCoinPriceAsync().ConfigureAwait(false);
            
            return Ok(result);
        }
    }
}
