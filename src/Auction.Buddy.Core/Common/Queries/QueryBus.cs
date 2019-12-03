using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Auction.Buddy.Core.Common.Queries
{
    public interface QueryBus
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query);
    }

    public class ServiceProviderQueryBus : QueryBus
    {
        private readonly IServiceProvider _provider;
        private readonly ILoggerFactory _loggerFactory;

        public ServiceProviderQueryBus(IServiceProvider provider, ILoggerFactory loggerFactory)
        {
            _provider = provider;
            _loggerFactory = loggerFactory;
        }

        public async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query)
        {
            var handler = _provider.GetRequiredService<QueryHandler<TQuery, TResult>>();
            var loggingHandler = new LoggingQueryHandler<TQuery, TResult>(handler, _loggerFactory);
            return await loggingHandler.HandleAsync(query).ConfigureAwait(false);
        }
    }
}