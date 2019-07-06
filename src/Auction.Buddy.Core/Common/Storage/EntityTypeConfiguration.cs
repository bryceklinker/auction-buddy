using Auction.Buddy.Core.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Buddy.Core.Common.Storage
{
    public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
        where T : class, IEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}