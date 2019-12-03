using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Persistence.Common.Storage;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Core.Test.Support.Storage
{
    public class InMemoryReadStore : EntityFrameworkReadStore
    {
        public InMemoryReadStore()
            : base(CreateInMemoryOptions())
        {
            
        }
        private static DbContextOptions<InMemoryReadStore> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<InMemoryReadStore>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options;
        }
    }
}