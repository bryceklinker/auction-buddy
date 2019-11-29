using System.Linq;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Test.Support.Storage
{
    public class InMemoryEventStore : DefaultEventStore
    {
        public InMemoryEventStore() 
            : base(new InMemoryEventPersistence())
        {
        }

        public DomainEvent<TId> GetLastEvent<TId>(TId id) 
            where TId : Identity
        {
            return GetEventsByIdAsync(id).Result.Last();
        }
    }
}