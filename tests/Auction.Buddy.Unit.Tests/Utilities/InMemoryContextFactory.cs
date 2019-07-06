using System;
using Auction.Buddy.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Unit.Tests.Utilities
{
    public static class InMemoryContextFactory
    {
        public static AuctionContext Create(string name = default(string))
        {
            var options = new DbContextOptionsBuilder<AuctionContext>()
                .UseInMemoryDatabase(name ?? $"{Guid.NewGuid()}")
                .Options;
            return new AuctionContext(options);
        }
    }
}