using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Storage
{
    public interface EventPersistence
    {
        Task PersistAsync(Identity identity, IEnumerable<PersistenceEvent> events);

        Task<PersistenceEvent[]> LoadEventsAsync(Identity identity);

        Task<bool> ExistsAsync(Identity identity);
    }
}