using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Core.Test.Support.Storage
{
    public class InMemoryReadStore : DbContext, ReadStore
    {
        public InMemoryReadStore()
            : base(CreateInMemoryOptions())
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

        public IQueryable<T> GetAll<T>()
            where T : class
        {
            return Set<T>();
        }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }

        private static DbContextOptions<InMemoryReadStore> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<InMemoryReadStore>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var auctionReadModelBuilder = modelBuilder.Entity<AuctionReadModel>();
            auctionReadModelBuilder.HasKey(a => a.Id);
            auctionReadModelBuilder.Property(a => a.Name).IsRequired().HasMaxLength(256);
            auctionReadModelBuilder.Property(a => a.AuctionDate).IsRequired();

            var auctionItemReadModelBuilder = modelBuilder.Entity<AuctionItemReadModel>();
            auctionItemReadModelBuilder.HasKey(i => i.Id);
            auctionItemReadModelBuilder.Property(i => i.AuctionId).IsRequired();
            auctionItemReadModelBuilder.Property(i => i.Description).HasMaxLength(2048);
            auctionItemReadModelBuilder.Property(i => i.Donor).IsRequired().HasMaxLength(256);
            auctionItemReadModelBuilder.Property(i => i.Name).IsRequired().HasMaxLength(256);
            auctionItemReadModelBuilder.Property(i => i.Quantity).IsRequired();

            auctionItemReadModelBuilder.HasOne(i => i.Auction)
                .WithMany(a => a.Items)
                .HasForeignKey(i => i.AuctionId)
                .IsRequired();
            auctionItemReadModelBuilder.HasIndex(i => new {i.AuctionId, i.Name}).IsUnique();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}