using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Test.Support.Events
{
    public class TestingMultipleHandlersDomainEvent : DomainEventBase<TestingId>
    {
        public int TimesHandled { get; private set; }

        public TestingMultipleHandlersDomainEvent(TestingId aggregateId) 
            : base(aggregateId, DateTimeOffset.UtcNow)
        {
        }

        public void Handle()
        {
            TimesHandled++;
        }
    }
    
    public class FirstTestingMultipleHandlersDomainEvent : DomainEventHandler<TestingMultipleHandlersDomainEvent, TestingId>
    {
        public Task Handle(TestingMultipleHandlersDomainEvent @event)
        {
            @event.Handle();
            return Task.CompletedTask;
        }
    }
    
    public class SecondTestingMultipleHandlersDomainEvent : DomainEventHandler<TestingMultipleHandlersDomainEvent, TestingId>
    {
        public Task Handle(TestingMultipleHandlersDomainEvent @event)
        {
            @event.Handle();
            return Task.CompletedTask;
        }
    }
    
    public class ThirdTestingMultipleHandlersDomainEvent : DomainEventHandler<TestingMultipleHandlersDomainEvent, TestingId>
    {
        public Task Handle(TestingMultipleHandlersDomainEvent @event)
        {
            @event.Handle();
            return Task.CompletedTask;
        }
    }
}