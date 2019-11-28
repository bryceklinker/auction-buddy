using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;

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
}