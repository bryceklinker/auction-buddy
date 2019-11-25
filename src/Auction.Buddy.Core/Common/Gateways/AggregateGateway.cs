using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Gateways
{
    public interface AggregateGateway<TAggregate, in TId>
        where TAggregate : AggregateRoot<TId>
        where TId : Identity
    {
        Task<TAggregate> FindByIdAsync(TId id);
        Task CommitAsync(TAggregate aggregate);
    }
}