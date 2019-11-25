using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEventHandler<in TAggregate, TId, in TEvent>
        where TAggregate : AggregateRoot<TId> 
        where TId : Identity
        where TEvent : DomainEvent<TId>
    {
        Task Handle(TAggregate aggregate, TEvent @event);
    }
}