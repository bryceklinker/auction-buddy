using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Auction.Buddy.Core.Common.Commands
{
    public class LoggingCommandHandler<TCommand> : CommandHandler<TCommand>
    {
        private readonly CommandHandler<TCommand> _innerHandler;
        private readonly ILogger _logger;

        private static string CommandName => typeof(TCommand).Name;
        
        public LoggingCommandHandler(CommandHandler<TCommand> innerHandler, ILoggerFactory loggerFactory)
        {
            _innerHandler = innerHandler;
            _logger = loggerFactory.CreateLogger<LoggingCommandHandler<TCommand>>();
        }

        public async Task<CommandResult> HandleAsync(TCommand command)
        {
            try
            {
                _logger.LogInformation($"Executing command {CommandName}...");
                var result = await _innerHandler.HandleAsync(command);
                _logger.LogInformation($"Finished executing command {CommandName}.");
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Command {CommandName} failed.");
                throw;
            }
        }
    }
    
    public class LoggingCommandHandler<TCommand, TResult> : CommandHandler<TCommand, TResult>
    {
        private readonly CommandHandler<TCommand, TResult> _innerHandler;
        private readonly ILogger _logger;

        private static string CommandName => typeof(TCommand).Name;
        
        public LoggingCommandHandler(CommandHandler<TCommand, TResult> innerHandler, ILoggerFactory loggerFactory)
        {
            _innerHandler = innerHandler;
            _logger = loggerFactory.CreateLogger<LoggingCommandHandler<TCommand, TResult>>();
        }

        public async Task<CommandResult<TResult>> HandleAsync(TCommand command)
        {
            try
            {
                _logger.LogInformation($"Executing command {CommandName}...");
                var result = await _innerHandler.HandleAsync(command);
                _logger.LogInformation($"Command {CommandName} finished.");
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Command {CommandName} failed.");
                throw;
            }
        }
    }
}