using System.Threading.Tasks;
using Auction.Buddy.Core.Authentication;
using Auction.Buddy.Core.Authentication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Buddy.Web.Authentication
{
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticator _authenticator;

        public AuthenticationController(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsDto dto)
        {
            return Ok(await _authenticator.Authenticate(dto));
        }
    }
}