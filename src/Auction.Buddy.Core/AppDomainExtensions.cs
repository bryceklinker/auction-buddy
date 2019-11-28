using System;
using System.Collections.Generic;
using System.Linq;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.DependencyInjection;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core
{
    public static class AppDomainExtensions
    {
        public static IEnumerable<ServiceRegistration> GetServiceRegistrations(this AppDomain appDomain)
        {
            return appDomain.GetServiceRegistrations(typeof(CommandHandler<>))
                .Concat(appDomain.GetServiceRegistrations(typeof(CommandHandler<,>)))
                .Concat(appDomain.GetServiceRegistrations(typeof(DomainEventHandler<,>)));
        }

        private static IEnumerable<ServiceRegistration> GetServiceRegistrations(this AppDomain appDomain,
            Type genericInterfaceType)
        {
            return appDomain.GetAssemblies()
                .SelectMany(a => a.GetServiceRegistrations(genericInterfaceType));
        }
    }
}