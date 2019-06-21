using Harvest.Home.Core.Common.Storage;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harvest.Home.Core.Auctions.Entities
{
    public class AuctionEntityConfiguration : EntityConfiguration<AuctionEntity>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<AuctionEntity> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(StorageDefaults.MaxLength);
            builder.Property(p => p.AuctionDate).IsRequired();
        }
    }
}