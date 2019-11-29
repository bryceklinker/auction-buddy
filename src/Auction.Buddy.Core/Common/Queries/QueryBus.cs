using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Buddy.Core.Common.Queries
{
    public interface QueryBus
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query);
    }

    public class ServiceProviderQueryBus : QueryBus
    {
        private readonly IServiceProvider _provider;

        public ServiceProviderQueryBus(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query)
        {
            var handler = _provider.GetRequiredService<QueryHandler<TQuery, TResult>>();
            return await handler.HandleAsync(query).ConfigureAwait(false);
        }
    }
}