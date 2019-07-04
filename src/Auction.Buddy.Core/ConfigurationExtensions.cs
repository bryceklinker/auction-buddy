using Microsoft.Extensions.Configuration;

namespace Auction.Buddy.Core
{
    public static class ConfigurationExtensions
    {
        public static string IdentityAuthority(this IConfiguration config)
        {
            return config["Identity:Authority"];
        }
        
        public static string IdentityAudience(this IConfiguration config)
        {
            return config["Identity:Audience"];
        }

        public static string IdentityClientId(this IConfiguration config)
        {
            return config["Identity:ClientId"];
        }

        public static string IdentityClientSecret(this IConfiguration config)
        {
            return config["Identity:ClientSecret"];
        }

        public static string IdentityScope(this IConfiguration config)
        {
            return config["Identity:Scope"];
        }
    }
}