using Auction.Buddy.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Buddy.Persistence.Common.Storage
{
    public class PersistenceEventConfiguration : IEntityTypeConfiguration<PersistenceEvent>
    {
        public void Configure(EntityTypeBuilder<PersistenceEvent> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Timestamp).IsRequired();
            builder.Property(e => e.AggregateId).IsRequired();
            builder.Property(e => e.EventName).IsRequired();
            builder.Property(e => e.SerializedEvent).IsRequired();
        }
    }
}