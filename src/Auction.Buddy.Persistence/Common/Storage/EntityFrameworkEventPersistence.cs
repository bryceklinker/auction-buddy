using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Persistence.Common.Storage
{
    public class EntityFrameworkEventPersistence : DbContext, EventPersistence
    {
        public EntityFrameworkEventPersistence(DbContextOptions<EntityFrameworkEventPersistence> options)
            : base(options)
        {
            
        }
        public async Task PersistAsync(Identity identity, IEnumerable<PersistenceEvent> events)
        {
            AddRange(events);
            await SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<PersistenceEvent[]> LoadEventsAsync(Identity identity)
        {
            return await Set<PersistenceEvent>()
                .Where(e => e.AggregateId == identity.ToString())
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task<bool> ExistsAsync(Identity identity)
        {
            return await Set<PersistenceEvent>()
                .AnyAsync(e => e.AggregateId == identity.ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersistenceEventConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}