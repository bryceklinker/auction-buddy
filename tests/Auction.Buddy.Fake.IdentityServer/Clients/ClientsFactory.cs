using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Auction.Buddy.Fake.IdentityServer.Clients
{
    public static class ClientsFactory
    {
        public static IEnumerable<Client> Create()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "auction.buddy.web",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("auction.buddy.web.secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        "auction_buddy",
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId
                    }
                }
            };
        }
    }
}