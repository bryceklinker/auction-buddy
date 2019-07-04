using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Auction.Buddy.Core.Authentication;
using FluentAssertions;
using Xunit;

namespace Auction.Buddy.Unit.Tests.Authentication
{
    public class AuthenticationResultFactoryTests
    {
        private readonly HttpResponseMessage _response;
        private readonly AuthenticationResultFactory _authenticationResultFactory;

        public AuthenticationResultFactoryTests()
        {
            const string json = "{ 'access_token': 'header.payload.signature', 'token_type': 'Bearer', 'expires_in': 2000 }";
            _response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            _authenticationResultFactory = new AuthenticationResultFactory();
        }

        [Fact]
        public async Task GivenUnsuccessfulResponseWhenCreateResultThenIsSuccessfulIsFalse()
        {
            _response.StatusCode = HttpStatusCode.BadRequest;

            var result = await _authenticationResultFactory.CreateResult(_response);
            result.IsSuccess.Should().BeFalse();
        }
        
        [Fact]
        public async Task GivenSuccessfulResponseWhenCreateResultThenIsSuccessfulIsTrue()
        {
            var result = await _authenticationResultFactory.CreateResult(_response);
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task GivenSuccessfulResponseWhenCreateResultThenResultHasAccessToken()
        {
            var result = await _authenticationResultFactory.CreateResult(_response);
            result.AccessToken.Should().Be("header.payload.signature");
            result.ExpiresIn.Should().Be(2000);
            result.TokenType.Should().Be("Bearer");
        }
    }
}