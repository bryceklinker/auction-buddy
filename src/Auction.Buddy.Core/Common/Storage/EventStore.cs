using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;
using Newtonsoft.Json;

namespace Auction.Buddy.Core.Common.Storage
{
    public interface EventStore
    {
        Task CommitAsync<TId>(TId id, IEnumerable<DomainEvent<TId>> events)
            where TId : Identity;

        Task<DomainEvent<TId>[]> GetEventsByIdAsync<TId>(TId id) 
            where TId : Identity;

        Task<TAggregate> LoadAggregateAsync<TAggregate, TId>(TId id)
            where TAggregate : AggregateRoot<TId>
            where TId : Identity;
    }
    
    public class DefaultEventStore : EventStore
    {
        private readonly EventPersistence _persistence;
        private readonly DomainEventSerializer _serializer;

        public DefaultEventStore(EventPersistence persistence)
            : this(persistence, new JsonDomainEventSerializer())
        {
            
        }
        
        public DefaultEventStore(EventPersistence persistence, DomainEventSerializer serializer)
        {
            _persistence = persistence;
            _serializer = serializer;
        }
        
        public async Task CommitAsync<TId>(TId id, IEnumerable<DomainEvent<TId>> events) 
            where TId : Identity
        {
            var persistenceEvents = events.Select(e => e.ToPersistenceEvent(_serializer)).ToArray();
            await _persistence.PersistAsync(id, persistenceEvents).ConfigureAwait(false);
        }

        public async Task<DomainEvent<TId>[]> GetEventsByIdAsync<TId>(TId id) 
            where TId : Identity
        {
            var persistedEvents = await _persistence.LoadEventsAsync(id);
            return persistedEvents
                .Select(p =>_serializer.Deserialize<TId>(p))
                .ToArray();
        }

        public async Task<TAggregate> LoadAggregateAsync<TAggregate, TId>(TId id) 
            where TAggregate : AggregateRoot<TId> 
            where TId : Identity
        {
            var isExisting = await _persistence.ExistsAsync(id);
            if (!isExisting)
                return default;

            var aggregate = (TAggregate) Activator.CreateInstance(typeof(TAggregate), id);
            await aggregate.LoadAsync(this);
            return aggregate;
        }
    }
}