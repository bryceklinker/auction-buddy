using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Auction.Buddy.Core.Common.Commands
{
    public interface CommandBus
    {
        Task<CommandResult> ExecuteAsync<TCommand>(TCommand command);
        Task<CommandResult<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command);
    }

    public class ServiceProviderCommandBus : CommandBus
    {
        private readonly IServiceProvider _provider;
        private readonly ILoggerFactory _loggerFactory;

        public ServiceProviderCommandBus(IServiceProvider provider, ILoggerFactory loggerFactory)
        {
            _provider = provider;
            _loggerFactory = loggerFactory;
        }

        public async Task<CommandResult> ExecuteAsync<TCommand>(TCommand command)
        {
            var handler = _provider.GetRequiredService<CommandHandler<TCommand>>();
            var loggingHandler = new LoggingCommandHandler<TCommand>(handler, _loggerFactory);
            return await loggingHandler.HandleAsync(command)
                .ConfigureAwait(false);
        }

        public async Task<CommandResult<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command)
        {
            var handler = _provider.GetRequiredService<CommandHandler<TCommand, TResult>>();
            var loggingHandler = new LoggingCommandHandler<TCommand, TResult>(handler, _loggerFactory);
            return await loggingHandler.HandleAsync(command)
                .ConfigureAwait(false);
        }
    }
}