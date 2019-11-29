using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Queries
{
    public interface QueryHandler<in TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}