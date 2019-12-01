using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Core.Auctions
{
    public static class ReadStoreExtensions
    {
        public static async Task<AuctionReadModel> GetAuctionAsync(this ReadStore store, string auctionId)
        {
            return await store.GetAll<AuctionReadModel>()
                .SingleAsync(a => a.Id == auctionId);
        } 
        
        public static async Task<AuctionItemReadModel> GetAuctionItemAsync(this ReadStore store,
            string auctionId, string name)
        {
            return await store.GetAll<AuctionItemReadModel>()
                .Where(i => i.AuctionId == auctionId)
                .SingleAsync(i => i.Name == name);
        }
    }
}