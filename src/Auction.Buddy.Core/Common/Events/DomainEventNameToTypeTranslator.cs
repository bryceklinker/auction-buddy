using System;
using System.Linq;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEventNameToTypeTranslator
    {
        Type GetDomainEventType(string name);
    }

    public class TypeNameDomainEventNameToTypeTranslator : DomainEventNameToTypeTranslator
    {
        public Type GetDomainEventType(string name)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Single(t => t.Name == name);
        }
    }
}