using System;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Test.Support.Events
{
    public class TestingId : IdentityBase
    {
        public TestingId(string id = null) 
            : base("testing", id ?? $"{Guid.NewGuid()}")
        {
        }
    }

    public class TestingAggregate : AggregateRootBase<TestingId>
    {
        public TestingAggregate(TestingId id) : base(id)
        {
        }

        protected override void Apply(DomainEvent<TestingId> domainEvent)
        {
            
        }
    }
}