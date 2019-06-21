using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Harvest.Home.Core.Common.Storage
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : class, IEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);
            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}