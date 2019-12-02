using Auction.Buddy.Core.Auctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Buddy.Persistence.Auctions
{
    public class AuctionReadModelConfiguration : IEntityTypeConfiguration<AuctionReadModel>
    {
        public void Configure(EntityTypeBuilder<AuctionReadModel> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(256);
            builder.Property(a => a.AuctionDate).IsRequired();

            builder.HasMany(a => a.Items)
                .WithOne(i => i.Auction)
                .HasForeignKey(i => i.AuctionId)
                .IsRequired();
        }
    }
}