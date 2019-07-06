using Auction.Buddy.Core.Common.Storage;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Buddy.Core.Auctions.Entities
{
    public class AuctionEntityConfiguration : EntityTypeConfiguration<AuctionEntity>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<AuctionEntity> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(256);
            builder.Property(e => e.AuctionDate).IsRequired();
        }
    }
}