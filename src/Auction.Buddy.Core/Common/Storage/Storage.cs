using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Harvest.Home.Core.Common.Storage
{
    public interface IStorage
    {
        IQueryable<T> GetAll<T>() where T : class, IEntity;
        void AddEntity<T>(T entity) where T : class, IEntity;
        void DeleteEntity<T>(T entity) where T : class, IEntity;
        Task<T> FindEntityById<T>(long id) where T : class, IEntity;
        Task SaveAsync();
    }

    public class Storage : IStorage
    {
        private readonly StorageContext _context;

        public Storage(StorageContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll<T>()
            where T : class, IEntity
        {
            return _context.Set<T>();
        }

        public void AddEntity<T>(T entity) where T : class, IEntity
        {
            _context.Add(entity);
        }

        public void DeleteEntity<T>(T entity) where T : class, IEntity
        {
            _context.Remove(entity);
        }

        public Task<T> FindEntityById<T>(long id) where T : class, IEntity
        {
            return _context.FindAsync<T>(id);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        
    }
}