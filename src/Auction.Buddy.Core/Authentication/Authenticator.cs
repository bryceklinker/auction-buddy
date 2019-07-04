using System.Net.Http;
using System.Threading.Tasks;
using Auction.Buddy.Core.Authentication.Dtos;

namespace Auction.Buddy.Core.Authentication
{
    public interface IAuthenticator
    {
        Task<AuthenticationResultDto> Authenticate(CredentialsDto credentials);
    }

    public class Authenticator : IAuthenticator
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IAuthenticationRequestFactory _requestFactory;
        private readonly IAuthenticationResultFactory _resultFactory;

        public Authenticator(IHttpClientFactory clientFactory, IAuthenticationRequestFactory requestFactory, IAuthenticationResultFactory resultFactory)
        {
            _clientFactory = clientFactory;
            _requestFactory = requestFactory;
            _resultFactory = resultFactory;
        }

        public async Task<AuthenticationResultDto> Authenticate(CredentialsDto credentials)
        {
            var request = _requestFactory.CreateRequest(credentials);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return await _resultFactory.CreateResult(response);
        }
    }
}