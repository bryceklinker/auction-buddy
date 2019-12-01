using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEventHandler<in TEvent, TId> 
        where TId : Identity
        where TEvent : DomainEvent<TId>
    {
        Task HandleAsync(TEvent @event);
    }
}