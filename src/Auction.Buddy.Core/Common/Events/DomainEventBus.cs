using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEventBus
    {
        Task PublishAsync<TAggregate, TId>(TAggregate aggregate, DomainEvent<TId> @event)
            where TAggregate : AggregateRoot<TId>
            where TId : Identity;
    }
    
    public class NullDomainEventBus : DomainEventBus
    {
        public Task PublishAsync<TAggregate, TId>(TAggregate aggregate, DomainEvent<TId> @event)
            where TAggregate : AggregateRoot<TId>
            where TId : Identity
        {
            return Task.CompletedTask;
        }
    }
}