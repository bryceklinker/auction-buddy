using System;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Queries;
using Auction.Buddy.Core.Common.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Buddy.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuctionBuddy(this IServiceCollection services)
        {
            foreach (var registration in AppDomain.CurrentDomain.GetServiceRegistrations())
                services.AddTransient(registration.InterfaceType, registration.ServiceType);
            
            return services
                .AddTransient<CommandBus, ServiceProviderCommandBus>()
                .AddTransient<DomainEventBus, ServiceProviderDomainEventBus>()
                .AddTransient<QueryBus, ServiceProviderQueryBus>()
                .AddTransient<EventStore, DefaultEventStore>();
        }
    }
}