using System;
using Auction.Buddy.Persistence.Common.Storage;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Core.Test.Support.Storage
{
    public class InMemoryEventPersistence : EntityFrameworkEventPersistence
    {
        public InMemoryEventPersistence() 
            : base(new DbContextOptionsBuilder<EntityFrameworkEventPersistence>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options)
        {
        }
    }
}