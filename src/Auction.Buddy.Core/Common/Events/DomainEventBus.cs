using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        private readonly ILoggerFactory _loggerFactory;

        public ServiceProviderDomainEventBus(IServiceProvider provider, ILoggerFactory loggerFactory)
        {
            _provider = provider;
            _loggerFactory = loggerFactory;
        }

        public async Task PublishAsync<TEvent, TId>(TEvent @event) 
            where TEvent : DomainEvent<TId> 
            where TId : Identity
        {
            var handlers = _provider.GetServices<DomainEventHandler<TEvent, TId>>()
                .Select(h => new LoggingDomainEventHandler<TEvent, TId>(h, _loggerFactory));
            
            foreach (var handler in handlers) 
                await handler.HandleAsync(@event).ConfigureAwait(false);
        }
    }
}