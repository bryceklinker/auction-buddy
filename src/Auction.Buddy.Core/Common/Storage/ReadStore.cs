using System.Linq;
using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Storage
{
    public interface ReadStore
    {
        void Add<T>(T entity)
            where T : class;
        void Remove<T>(T entity)
            where T : class;
        IQueryable<T> GetAll<T>()
            where T : class;
        Task SaveAsync();
    }
}