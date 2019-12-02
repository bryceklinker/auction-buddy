using Auction.Buddy.Core.Auctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Buddy.Persistence.Auctions
{
    public class AuctionItemReadModelConfiguration : IEntityTypeConfiguration<AuctionItemReadModel>
    {
        public void Configure(EntityTypeBuilder<AuctionItemReadModel> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Description).HasMaxLength(4096);
            builder.Property(i => i.Donor).IsRequired().HasMaxLength(256);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(256);
            builder.Property(i => i.Quantity).IsRequired();

            builder.HasOne(i => i.Auction)
                .WithMany(a => a.Items)
                .HasForeignKey(i => i.AuctionId)
                .IsRequired();
        }
    }
}