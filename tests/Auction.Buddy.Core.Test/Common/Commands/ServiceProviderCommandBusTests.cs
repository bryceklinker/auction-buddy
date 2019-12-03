using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Test.Support.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Auction.Buddy.Core.Test.Common.Commands
{
    public class ServiceProviderCommandBusTests
    {
        private readonly ServiceProviderCommandBus _commandBus;

        public ServiceProviderCommandBusTests()
        {
            var provider = new ServiceCollection()
                .AddAuctionBuddy()
                .BuildServiceProvider();
            
            _commandBus = new ServiceProviderCommandBus(provider, new NullLoggerFactory());
        }

        [Fact]
        public async Task WhenCommandIsExecutedThenCommandIsPassedToTheHandler()
        {
            var command = new CommandWithoutResult();
            
            var result = await _commandBus.ExecuteAsync(command);

            Assert.True(result.WasSuccessful);
            Assert.True(command.WasHandled);
        }

        [Fact]
        public async Task WhenCommandWithResultIsExecutedThenReturnsResultFromHandler()
        {
            var command = new CommandWithResult(Guid.NewGuid());

            var result = await _commandBus.ExecuteAsync<CommandWithResult, object>(command);
    
            Assert.Equal(command.Result, result.Result);
        }
    }
}