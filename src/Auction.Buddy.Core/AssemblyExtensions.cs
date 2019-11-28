using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Auction.Buddy.Core.Common.DependencyInjection;

namespace Auction.Buddy.Core
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<ServiceRegistration> GetServiceRegistrations(this Assembly assembly, Type genericInterfaceType)
        {
            return assembly.GetTypes()
                .Where(t => t.DoesImplementGenericInterface(genericInterfaceType))
                .SelectMany(t => t.ToServiceRegistrations(genericInterfaceType));
        }
    }
}