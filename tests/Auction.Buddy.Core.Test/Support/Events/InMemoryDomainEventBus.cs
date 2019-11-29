using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Test.Support.Events
{
    public class InMemoryDomainEventBus : DomainEventBus
    {
        private readonly List<object> _publishedEvents = new List<object>();
        
        public Task PublishAsync<TEvent, TId>(TEvent @event) 
            where TEvent : DomainEvent<TId> 
            where TId : Identity
        {
            _publishedEvents.Add(@event);
            return Task.CompletedTask;
        }

        public TEvent[] GetPublishedEvent<TEvent, TId>()
            where TEvent : DomainEvent<TId> 
            where TId : Identity
        {
            return _publishedEvents.OfType<TEvent>().ToArray();
        }
    }
}