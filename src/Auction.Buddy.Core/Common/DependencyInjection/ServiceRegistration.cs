using System;

namespace Auction.Buddy.Core.Common.DependencyInjection
{
    public class ServiceRegistration
    {
        public Type InterfaceType { get; }
        public Type ServiceType { get; }

        public ServiceRegistration(Type interfaceType, Type serviceType)
        {
            InterfaceType = interfaceType;
            ServiceType = serviceType;
        }
    }
}