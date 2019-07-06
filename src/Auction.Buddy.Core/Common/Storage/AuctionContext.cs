using Auction.Buddy.Core.Auctions.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Core.Common.Storage
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}