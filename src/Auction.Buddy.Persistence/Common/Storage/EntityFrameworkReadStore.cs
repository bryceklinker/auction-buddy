using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Persistence.Auctions;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Persistence.Common.Storage
{
    public class EntityFrameworkReadStore : DbContext, ReadStore
    {
        public EntityFrameworkReadStore(DbContextOptions options)
            : base(options)
        {
            
        }
        
        void ReadStore.Add<T>(T entity)
        {
            base.Add(entity);
        }

        void ReadStore.Remove<T>(T entity)
        {
            base.Remove(entity);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return Set<T>();
        }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuctionItemReadModelConfiguration())
                .ApplyConfiguration(new AuctionReadModelConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}