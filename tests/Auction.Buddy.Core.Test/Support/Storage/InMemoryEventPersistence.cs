using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Test.Support.Storage
{
    public class InMemoryEventPersistence : EventPersistence
    {
        private readonly Dictionary<Identity, List<PersistenceEvent>> _storedEvents 
            = new Dictionary<Identity, List<PersistenceEvent>>();
        
        public Task PersistAsync(Identity identity, IEnumerable<PersistenceEvent> events)
        {
            var existing = GetEventsByIdentity(identity);
            existing.AddRange(events);
            return Task.CompletedTask;
        }

        public Task<PersistenceEvent[]> LoadEventsAsync(Identity identity)
        {
            return Task.FromResult(GetEventsByIdentity(identity).ToArray());
        }

        public Task<bool> ExistsAsync(Identity identity)
        {
            var exists = identity != null && _storedEvents.ContainsKey(identity);
            return Task.FromResult(exists);
        }

        private List<PersistenceEvent> GetEventsByIdentity(Identity identity)
        {
            if (_storedEvents.ContainsKey(identity))
                return _storedEvents[identity];
            
            return _storedEvents[identity] = new List<PersistenceEvent>();
        }
    }
}