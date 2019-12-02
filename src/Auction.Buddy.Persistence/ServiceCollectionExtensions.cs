using System;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Persistence.Common.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Buddy.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuctionBuddyEntityFramework(this IServiceCollection services,
            Action<DbContextOptionsBuilder> configureEventStore,
            Action<DbContextOptionsBuilder> configureReadStore)
        {
            return services
                .AddDbContext<EntityFrameworkEventPersistence>(configureEventStore)
                .AddDbContext<EntityFrameworkReadStore>(configureReadStore)
                .AddTransient<EventPersistence>(p => p.GetRequiredService<EntityFrameworkEventPersistence>())
                .AddTransient<ReadStore>(p => p.GetRequiredService<EntityFrameworkReadStore>());
        } 
    }
}