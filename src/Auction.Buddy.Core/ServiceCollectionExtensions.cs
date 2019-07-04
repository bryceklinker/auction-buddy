using Auction.Buddy.Core.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Buddy.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuctionBuddy(this IServiceCollection services)
        {
            return services.AddTransient<IAuthenticationRequestFactory, AuthenticationRequestFactory>()
                .AddTransient<IAuthenticationResultFactory, AuthenticationResultFactory>()
                .AddTransient<IAuthenticator, Authenticator>();
        }
    }
}