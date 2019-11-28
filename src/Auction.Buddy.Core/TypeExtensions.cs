using System;
using System.Collections.Generic;
using System.Linq;
using Auction.Buddy.Core.Common.DependencyInjection;

namespace Auction.Buddy.Core
{
    public static class TypeExtensions
    {
        public static bool DoesImplementGenericInterface(this Type type, Type genericInterfaceType)
        {
            return type.GetImplementedGenericInterfaceTypes(genericInterfaceType).Any();       
        }
        
        public static IEnumerable<ServiceRegistration> ToServiceRegistrations(this Type type, Type genericInterfaceType)
        {
            return type.GetImplementedGenericInterfaceTypes(genericInterfaceType) 
                .Select(it => new ServiceRegistration(it, type));
        }
        
        private static IEnumerable<Type> GetImplementedGenericInterfaceTypes(this Type type, Type genericInterfaceType)
        {
            if (type.IsGenericType)
                return Array.Empty<Type>();
            
            return type
                .GetInterfaces()
                .Where(it => it.IsConstructedGenericType)
                .Where(it => it.GetGenericTypeDefinition() == genericInterfaceType);
        }
    }
}