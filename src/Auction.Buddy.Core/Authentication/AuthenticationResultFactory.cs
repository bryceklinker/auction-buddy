using System.Net.Http;
using System.Threading.Tasks;
using Auction.Buddy.Core.Authentication.Dtos;
using Auction.Buddy.Core.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Auction.Buddy.Core.Authentication
{
    public interface IAuthenticationResultFactory
    {
        Task<AuthenticationResultDto> CreateResult(HttpResponseMessage response);
    }

    public class AuthenticationResultFactory : IAuthenticationResultFactory
    {
        private readonly ISystemClock _clock;
        private readonly ILogger _logger;

        public AuthenticationResultFactory(ILoggerFactory loggerFactory, ISystemClock clock)
        {
            _clock = clock;
            _logger = loggerFactory.CreateLogger<AuthenticationResultFactory>();
        }
        
        public async Task<AuthenticationResultDto> CreateResult(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode
                ? await CreateSuccessfulResult(response)
                : await CreateFailedResult(response);
        }

        private async Task<AuthenticationResultDto> CreateSuccessfulResult(HttpResponseMessage response)
        {
            var jObject = await response.Content.ReadAsAsync<JObject>();
            var expiresIn = jObject.Value<int>("expires_in");
            return new AuthenticationResultDto
            {
                IsSuccess = true,
                AccessToken = jObject.Value<string>("access_token"),
                ExpiresIn = expiresIn,
                ExpiresAt = _clock.UtcNow.AddSeconds(expiresIn),
                TokenType = jObject.Value<string>("token_type")
            };
        }

        private async Task<AuthenticationResultDto> CreateFailedResult(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogError("LoginView Failed: {content}", content);
            return new AuthenticationResultDto
            {
                IsSuccess = false
            };
        }
    }
}