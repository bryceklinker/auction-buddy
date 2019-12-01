using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Buddy.Core.Common.Events
{
    public interface DomainEventBus
    {
        Task PublishAsync<TEvent, TId>(TEvent @event)
            where TEvent : DomainEvent<TId>
            where TId : Identity;
    }

    public class ServiceProviderDomainEventBus : DomainEventBus
    {
        private readonly IServiceProvider _provider;

        public ServiceProviderDomainEventBus(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task PublishAsync<TEvent, TId>(TEvent @event) 
            where TEvent : DomainEvent<TId> 
            where TId : Identity
        {
            var handlers = _provider.GetServices<DomainEventHandler<TEvent, TId>>();
            foreach (var handler in handlers) 
                await handler.HandleAsync(@event).ConfigureAwait(false);
        }
    }
}