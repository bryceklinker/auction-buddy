using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Auction.Buddy.Core.Common.Queries
{
    public class LoggingQueryHandler<TQuery, TResult> : QueryHandler<TQuery, TResult>
    {
        private readonly QueryHandler<TQuery, TResult> _innerHandler;
        private readonly ILogger _logger;

        private static string QueryName => typeof(TQuery).Name;

        public LoggingQueryHandler(QueryHandler<TQuery, TResult> innerHandler, ILoggerFactory loggerFactory)
        {
            _innerHandler = innerHandler;
            _logger = loggerFactory.CreateLogger<LoggingQueryHandler<TQuery, TResult>>();
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            try
            {
                _logger.LogInformation($"Executing query {QueryName}...");
                var result = await _innerHandler.HandleAsync(query);
                _logger.LogInformation($"Finished executing query {QueryName}.");
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Query {QueryName} failed.");
                throw;
            }
        }
    }
}