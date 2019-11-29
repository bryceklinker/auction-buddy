using System;
using Auction.Buddy.Core.Common.Storage;
using Newtonsoft.Json;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEvent<out TId>
        where TId : Identity
    {
        TId AggregateId { get; }
        DateTimeOffset Timestamp { get; }

        PersistenceEvent ToPersistenceEvent(DomainEventSerializer serializer);
    }
    
    public abstract class DomainEventBase<TId> : DomainEvent<TId>
        where TId : Identity
    {
        public TId AggregateId { get; }
        
        public DateTimeOffset Timestamp { get; }

        protected DomainEventBase(TId aggregateId, DateTimeOffset? timestamp = null)
        {
            Timestamp = timestamp ?? DateTimeOffset.UtcNow;
            AggregateId = aggregateId;
        }

        public virtual PersistenceEvent ToPersistenceEvent(DomainEventSerializer serializer)
        {
            return new PersistenceEvent
            {
                Timestamp = Timestamp,
                AggregateId = AggregateId.ToString(),
                EventName = GetType().Name,
                SerializedEvent = serializer.Serialize(this)
            };
        }
    }
}