using System;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Test.Support.Storage;

namespace Auction.Buddy.Core.Test.Auctions
{
    public static class InMemoryReadStoreExtensions
    {
        public static AuctionReadModel AddAuction(
            this InMemoryReadStore store, 
            AuctionId auctionId = null,
            string name = null,
            DateTimeOffset? auctionDate = null)
        {
            var entry = store.Add(new AuctionReadModel
            {
                Id = auctionId ?? new AuctionId(),
                Name = name ?? $"{Guid.NewGuid()}",
                AuctionDate = auctionDate.GetValueOrDefault(DateTimeOffset.UtcNow)
            });
            store.SaveChanges();
            return entry.Entity;
        }

        public static AuctionItemReadModel AddAuctionItem(
            this InMemoryReadStore store,
            AuctionId auctionId,
            string name,
            string donor = null,
            string description = null,
            int quantity = 1)
        {
            var entry = store.Add(new AuctionItemReadModel
            {
                Description = description,
                Donor = donor,
                AuctionId = auctionId,
                Name = name,
                Quantity = quantity
            });
            store.SaveChanges();
            return entry.Entity;
        }
    }
}