using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Storage;
using Microsoft.Extensions.Logging;

namespace Auction.Buddy.Core.Common.Events
{
    public class LoggingDomainEventHandler<TEvent, TId> : DomainEventHandler<TEvent, TId> where TEvent : DomainEvent<TId> where TId : Identity
    {
        private readonly DomainEventHandler<TEvent, TId> _innerHandler;
        private readonly ILogger _logger;

        private static string EventName => typeof(TEvent).Name;

        public LoggingDomainEventHandler(DomainEventHandler<TEvent, TId> innerHandler, ILoggerFactory loggerFactory)
        {
            _innerHandler = innerHandler;
            _logger = loggerFactory.CreateLogger<LoggingDomainEventHandler<TEvent, TId>>();
        }

        public async Task HandleAsync(TEvent @event)
        {
            try
            {
                _logger.LogInformation($"Handling event {EventName}...");
                await _innerHandler.HandleAsync(@event);
                _logger.LogInformation($"Finished handling event {EventName}.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Event {EventName} was not handled successfully.");
                throw;
            }
        }
    }
}