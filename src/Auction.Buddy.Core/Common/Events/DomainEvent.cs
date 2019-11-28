using System;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEvent<out TId>
        where TId : Identity
    {
        Guid Id { get; }
        TId AggregateId { get; }
        DateTimeOffset Timestamp { get; }
    }
    
    public abstract class DomainEventBase<TId> : DomainEvent<TId>
        where TId : Identity
    {
        public Guid Id { get; }
        public TId AggregateId { get; }
        public DateTimeOffset Timestamp { get; }

        protected DomainEventBase(TId aggregateId)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTimeOffset.UtcNow;
            
            AggregateId = aggregateId;
        }
    }
}