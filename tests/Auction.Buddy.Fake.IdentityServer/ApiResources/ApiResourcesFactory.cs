using System.Collections.Generic;
using IdentityServer4.Models;

namespace Auction.Buddy.Fake.IdentityServer.ApiResources
{
    public static class ApiResourcesFactory
    {
        public static IEnumerable<ApiResource> Create()
        {
            return new List<ApiResource>
            {
                new ApiResource("auction_buddy", "Auction Buddy")
            };
        }
    }
}