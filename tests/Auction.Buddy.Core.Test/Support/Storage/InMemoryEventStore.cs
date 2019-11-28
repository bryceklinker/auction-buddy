using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Test.Support.Storage
{
    public class InMemoryEventStore : EventStore
    {
        private readonly Dictionary<object, object> _savedEvents = new Dictionary<object, object>();
        
        public Task CommitAsync<TId>(TId id, IEnumerable<DomainEvent<TId>> events)
            where TId : Identity    
        {
            var aggregateEvents = GetSavedEventsById(id);
            aggregateEvents.AddRange(events);
            return Task.CompletedTask;
        }

        public void Commit<TId>(TId id, params DomainEvent<TId>[] events) 
            where TId : Identity
        {
            var aggregateEvents = GetSavedEventsById(id);
            aggregateEvents.AddRange(events);
        }

        public Task<DomainEvent<TId>[]> GetEventsByIdAsync<TId>(TId id) 
            where TId : Identity
        {
            return Task.FromResult(GetEventsById(id));
        }

        public DomainEvent<TId>[] GetEventsById<TId>(TId id) 
            where TId : Identity
        {
            return GetSavedEventsById(id).ToArray();
        }

        public async Task<TAggregate> LoadAggregateAsync<TAggregate, TId>(TId id) 
            where TAggregate : AggregateRoot<TId> 
            where TId : Identity
        {
            if (id == null || !_savedEvents.ContainsKey(id))
                return default;
            
            var aggregate = (TAggregate) Activator.CreateInstance(typeof(TAggregate), id);
            await aggregate.LoadAsync(this);
            return aggregate;
        }

        private List<DomainEvent<TId>> GetSavedEventsById<TId>(TId id) 
            where TId : Identity
        {
            if (!_savedEvents.ContainsKey(id)) _savedEvents[id] = new List<DomainEvent<TId>>();

            var aggregateEvents = (List<DomainEvent<TId>>) _savedEvents[id];
            return aggregateEvents;
        }
    }
}