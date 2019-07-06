using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Entities;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Auctions
{
    public interface IAuctionEntityRepository
    {
        Task<AuctionEntity> AddAsync(AuctionEntity entity);
    }

    public class AuctionEntityRepository : IAuctionEntityRepository
    {
        private readonly AuctionContext _context;

        public AuctionEntityRepository(AuctionContext context)
        {
            _context = context;
        }

        public async Task<AuctionEntity> AddAsync(AuctionEntity entity)
        {
            var entry = _context.Add(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }
    }
}