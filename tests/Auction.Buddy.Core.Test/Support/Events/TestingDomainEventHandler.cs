using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Test.Support.Events
{
    public class TestingDomainEvent : DomainEventBase<TestingId>
    {
        public int TimesHandled { get; private set; }
        public TestingDomainEvent(TestingId aggregateId) 
            : base(aggregateId)
        {
        }

        public void Handle()
        {
            TimesHandled++;
        }
    }
    
    public class TestingDomainEventHandler : DomainEventHandler<TestingDomainEvent, TestingId>
    {
        public Task Handle(TestingDomainEvent @event)
        {
            @event.Handle();
            return Task.CompletedTask;
        }
    }
}