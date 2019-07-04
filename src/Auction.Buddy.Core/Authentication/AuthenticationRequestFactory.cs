using System;
using System.Collections.Generic;
using System.Net.Http;
using Auction.Buddy.Core.Authentication.Dtos;
using Microsoft.Extensions.Configuration;

namespace Auction.Buddy.Core.Authentication
{
    public interface IAuthenticationRequestFactory
    {
        HttpRequestMessage CreateRequest(CredentialsDto credentials);
    }

    public class AuthenticationRequestFactory : IAuthenticationRequestFactory
    {
        private readonly IConfiguration _configuration;

        private string Authority => _configuration.IdentityAuthority();
        private string Scope => _configuration.IdentityScope();
        private string ClientId => _configuration.IdentityClientId();
        private string ClientSecret => _configuration.IdentityClientSecret();
        
        public AuthenticationRequestFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpRequestMessage CreateRequest(CredentialsDto credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials));

            var tokenUrl = new Uri($"{Authority}/connect/token");
            return new HttpRequestMessage(HttpMethod.Post, tokenUrl)
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", ClientId),
                    new KeyValuePair<string, string>("client_secret", ClientSecret),
                    new KeyValuePair<string, string>("scope", $"openid profile {Scope}"),
                    new KeyValuePair<string, string>("username", credentials?.Username),
                    new KeyValuePair<string, string>("password", credentials?.Password)
                })
            };
        }
    }
}