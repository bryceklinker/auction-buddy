using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Queries;

namespace Auction.Buddy.Core.Test.Support.Queries
{
    public class TestingQuery
    {
        public object Result { get; }

        public bool WasHandled { get; private set; }

        public TestingQuery(object result)
        {
            Result = result;
        }

        public void Handle()
        {
            WasHandled = true;
        }
    }

    public class TestingQueryHandler : QueryHandler<TestingQuery, object>
    {
        public Task<object> HandleAsync(TestingQuery query)
        {
            query.Handle();
            return Task.FromResult(query.Result);
        }
    }
}