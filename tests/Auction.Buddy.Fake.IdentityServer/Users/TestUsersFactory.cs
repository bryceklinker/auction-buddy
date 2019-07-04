using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Test;

namespace Auction.Buddy.Fake.IdentityServer.Users
{
    public static class TestUsersFactory
    {
        public static List<TestUser> Create()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    Password = "abc123!",
                    Username = "bill@somewhere.com",
                    Claims = new List<Claim>
                    {
                        new Claim("scope", "admin")
                    }
                }
            };    
        }
    }
}