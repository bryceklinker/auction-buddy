using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Persistence.Common.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Auction.Buddy.Api.Testing
{
    [ApiController]
    [Route("testing")]
    public class TestingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EntityFrameworkEventPersistence _eventPersistence;
        private readonly EntityFrameworkReadStore _readStore;

        private bool ShouldRejectTesting => !bool.TryParse(_configuration["AppSettings:EnableTesting"], out var result)
                                            || !result;


        public TestingController(IConfiguration configuration, EntityFrameworkEventPersistence eventPersistence, EntityFrameworkReadStore readStore)
        {
            _configuration = configuration;
            _eventPersistence = eventPersistence;
            _readStore = readStore;
        }

        [HttpDelete("clear-all")]
        public async Task<IActionResult> ClearAll()
        {
            if (ShouldRejectTesting)
                return NotFound();

            await ClearEventStore();
            await ClearReadStore();
            return Ok();
        }

        private async Task ClearEventStore()
        {
            await RemoveAllAsync<EntityFrameworkEventPersistence, PersistenceEvent>(_eventPersistence);
        }

        private async Task ClearReadStore()
        {
            await RemoveAllAsync<EntityFrameworkReadStore, AuctionItemReadModel>(_readStore);
            await RemoveAllAsync<EntityFrameworkReadStore, AuctionReadModel>(_readStore);
        }

        private async Task RemoveAllAsync<TContext, TEntity>(TContext context)
            where TContext : DbContext 
            where TEntity : class
        {
            var entities = await context.Set<TEntity>().ToArrayAsync();
            foreach (var entity in entities) context.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}