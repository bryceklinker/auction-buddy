using System;
using Auction.Buddy.Core.Common.Commands;
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
                .AddTransient<CommandBus, ServiceProviderCommandBus>();
        }
    }
}