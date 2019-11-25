using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Gateways;

namespace Auction.Buddy.Core.Test.Support.Gateways
{
    public class InMemoryAggregateGateway<TAggregate, TId> : AggregateGateway<TAggregate, TId> 
        where TAggregate : AggregateRoot<TId> 
        where TId : Identity
    {
        private readonly List<TAggregate> _aggregates = new List<TAggregate>();

        public TAggregate[] Aggregates => _aggregates.ToArray();
        
        public Task<TAggregate> FindByIdAsync(TId id)
        {
            var aggregate = _aggregates.Find(a => a.Id.Equals(id));
            return Task.FromResult(aggregate);
        }

        public async Task CommitAsync(TAggregate aggregate)
        {
            var existing = await FindByIdAsync(aggregate.Id);
            if (existing != null)
                _aggregates.Remove(existing);
            
            _aggregates.Add(aggregate);
            aggregate.Commit();
        }
    }
}