using System.Threading.Tasks;
using CoinPrice.Business.Service;
using CoinPrice.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CoinPrice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpPut("{coinType}")]
        public async Task<IActionResult> PutAsync(CoinType coinType)
        {
            await _userService.SetPreferredCoinAsync(coinType).ConfigureAwait(false);

            return Ok(coinType);
        }
    }
}
