using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Auction.Buddy.Core.Authentication;
using Auction.Buddy.Core.Authentication.Dtos;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Auction.Buddy.Unit.Tests.Authentication
{
    public class AuthenticationRequestFactoryTests
    {
        private readonly AuthenticationRequestFactory _authenticationRequestFactory;
        private readonly CredentialsDto _credentials;

        public AuthenticationRequestFactoryTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Identity:Authority", "https://hello.com"),
                    new KeyValuePair<string, string>("Identity:TokenEndpoint", "connect/token"),
                    new KeyValuePair<string, string>("Identity:ClientId", "auction.buddy"),
                    new KeyValuePair<string, string>("Identity:ClientSecret", "auction.buddy.secret"),
                    new KeyValuePair<string, string>("Identity:Scope", "admin"),
                    new KeyValuePair<string, string>("Identity:Audience", "https://somewhere.com"),
                })
                .Build();
            
            _credentials = new CredentialsDto
            {
                Password = "bill",
                Username = "bob"
            };
            _authenticationRequestFactory = new AuthenticationRequestFactory(configuration);
        }

        [Fact]
        public void GivenNullCredentialsWhenCreateRequestThenThrowsArgumentNullException()
        {
            Func<HttpRequestMessage> func = () => CreateRequest(null);
            func.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void GivenCredentialsWhenCreateRequestThenRequestUriIsTokenEndpoint()
        {
            var request = CreateRequest(_credentials);
            request.RequestUri.Should().Be(new Uri("https://hello.com/connect/token"));
        }

        [Fact]
        public void GivenCredentialsWhenCreateRequestThenMethodIsPost()
        {
            var request = CreateRequest(_credentials);
            request.Method.Should().Be(HttpMethod.Post);
        }

        [Fact]
        public async Task GivenCredentialsWhenCreateRequestThenContentHasIdentitySecrets()
        {
            var request = CreateRequest(_credentials);
            var formData = await request.Content.ReadAsFormDataAsync();
            formData["grant_type"].Should().Be("password");
            formData["client_id"].Should().Be("auction.buddy");
            formData["client_secret"].Should().Be("auction.buddy.secret");
            formData["scope"].Should().ContainAll("admin", "openid", "profile");
            formData["audience"].Should().ContainAll("https://somewhere.com");
        }

        [Fact]
        public async Task GivenCredentialsWhenCreateRequestThenUsernameAndPasswordIsInRequestForm()
        {
            var request = CreateRequest(_credentials);
            var formData = await request.Content.ReadAsFormDataAsync();
            formData["username"].Should().Be("bob");
            formData["password"].Should().Be("bill");
        }

        private HttpRequestMessage CreateRequest(CredentialsDto credentials = default(CredentialsDto))
        {
            return _authenticationRequestFactory.CreateRequest(credentials);
        }
    }
}