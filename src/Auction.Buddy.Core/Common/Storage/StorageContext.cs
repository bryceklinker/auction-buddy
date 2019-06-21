using Microsoft.EntityFrameworkCore;

namespace Harvest.Home.Core.Common.Storage
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Storage).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}