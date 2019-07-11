using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Dtos;
using Auction.Buddy.Core.Auctions.Entities;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Unit.Tests.Utilities;
using FluentAssertions;
using Xunit;

namespace Auction.Buddy.Unit.Tests.Auctions
{
    public class AuctionEntityRepositoryTests
    {
        private readonly AuctionContext _context;
        private readonly AuctionEntityRepository _repository;

        public AuctionEntityRepositoryTests()
        {
            _context = InMemoryContextFactory.Create();
            _repository = new AuctionEntityRepository(_context);
        }

        [Fact]
        public async Task GivenNullEntityWhenAddThenThrowsArgumentNullException()
        {
            await _repository.Awaiting(r => r.AddAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }
        
        [Fact]
        public async Task GivenEntityWhenAddThenEntityIsSavedToDatabase()
        {
            var entity = new AuctionEntity("one", DateTime.UtcNow);

            await _repository.AddAsync(entity);
            _context.Set<AuctionEntity>().Should().HaveCount(1);
        }

        [Fact]
        public async Task GivenAuctionsAreInTheDatabaseWhenGetAllThenAllAuctionsAreReturned()
        {
            _context.Add(new AuctionEntity("bob", DateTime.Today));
            _context.Add(new AuctionEntity("three", DateTime.Today));
            _context.Add(new AuctionEntity("four", DateTime.Today));
            _context.SaveChanges();

            var results = await _repository.GetAllDtosAsync();
            results.Should().HaveCount(3);
        }
    }
}