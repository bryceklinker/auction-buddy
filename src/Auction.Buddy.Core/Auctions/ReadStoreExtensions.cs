using System.Linq;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Auctions
{
    public static class ReadStoreExtensions
    {
        public static AuctionItemReadModel GetAuctionItem(this ReadStore source,
            string auctionId, string name)
        {
            return source.GetAll<AuctionItemReadModel>()
                .Where(i => i.AuctionId == auctionId)
                .Single(i => i.Name == name);
        }
    }
}