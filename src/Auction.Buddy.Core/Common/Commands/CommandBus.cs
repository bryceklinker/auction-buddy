using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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

        public ServiceProviderCommandBus(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<CommandResult> ExecuteAsync<TCommand>(TCommand command)
        {
            var handler = _provider.GetRequiredService<CommandHandler<TCommand>>();
            return await handler.HandleAsync(command)
                .ConfigureAwait(false);
        }

        public async Task<CommandResult<TResult>> ExecuteAsync<TCommand, TResult>(TCommand command)
        {
            var handler = _provider.GetRequiredService<CommandHandler<TCommand, TResult>>();

            return await handler.HandleAsync(command)
                .ConfigureAwait(false);
        }
    }
}