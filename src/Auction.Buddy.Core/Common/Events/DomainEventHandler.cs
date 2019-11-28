using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEventHandler<TId, in TEvent> 
        where TId : Identity
        where TEvent : DomainEvent<TId>
    {
        Task Handle(TEvent @event);
    }
}