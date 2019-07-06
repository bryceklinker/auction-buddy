using System;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Authentication;
using Auction.Buddy.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Buddy.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuctionBuddy(this IServiceCollection services, Action<DbContextOptionsBuilder> configureDbContext)
        {
            return services
                .AddHttpClient()
                .AddDbContext<AuctionContext>(configureDbContext)
                .AddTransient<IAuthenticationRequestFactory, AuthenticationRequestFactory>()
                .AddTransient<IAuthenticationResultFactory, AuthenticationResultFactory>()
                .AddTransient<IAuthenticator, Authenticator>()
                .AddTransient<IAuctionEntityFactory, AuctionEntityFactory>()
                .AddTransient<IAuctionEntityRepository, AuctionEntityRepository>()
                .AddTransient<ICreateAuctionInteractor, CreateAuctionInteractor>();
        }
    }
}