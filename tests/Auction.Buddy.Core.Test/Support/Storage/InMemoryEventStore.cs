using System.Linq;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Core.Test.Support.Events;

namespace Auction.Buddy.Core.Test.Support.Storage
{
    public class InMemoryEventStore : DefaultEventStore
    {
        public InMemoryEventStore() 
            : base(new InMemoryEventPersistence(), new InMemoryDomainEventBus())
        {
        }

        public DomainEvent<TId> GetLastEvent<TId>(TId id) 
            where TId : Identity
        {
            return GetEventsByIdAsync(id).Result.Last();
        }
    }
}