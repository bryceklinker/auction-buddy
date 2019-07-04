using System.Net.Http;
using System.Threading.Tasks;
using Auction.Buddy.Core.Authentication.Dtos;
using Newtonsoft.Json.Linq;

namespace Auction.Buddy.Core.Authentication
{
    public interface IAuthenticationResultFactory
    {
        Task<AuthenticationResultDto> CreateResult(HttpResponseMessage response);
    }

    public class AuthenticationResultFactory : IAuthenticationResultFactory
    {
        public async Task<AuthenticationResultDto> CreateResult(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode
                ? await CreateSuccessfulResult(response)
                : CreateFailedResult();
        }

        private static async Task<AuthenticationResultDto> CreateSuccessfulResult(HttpResponseMessage response)
        {
            var jObject = await response.Content.ReadAsAsync<JObject>();
            return new AuthenticationResultDto
            {
                IsSuccess = true,
                AccessToken = jObject.Value<string>("access_token"),
                ExpiresIn = jObject.Value<int>("expires_in"),
                TokenType = jObject.Value<string>("token_type")
            };
        }

        private static AuthenticationResultDto CreateFailedResult()
        {
            return new AuthenticationResultDto
            {
                IsSuccess = false
            };
        }
    }
}