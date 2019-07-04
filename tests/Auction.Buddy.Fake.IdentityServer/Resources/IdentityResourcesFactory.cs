using System.Collections.Generic;
using IdentityServer4.Models;

namespace Auction.Buddy.Fake.IdentityServer.Resources
{
    public static class IdentityResourcesFactory
    {
        public static IEnumerable<IdentityResource> Create()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.Profile(),
                new IdentityResources.OpenId()
            };
        }
    }
}