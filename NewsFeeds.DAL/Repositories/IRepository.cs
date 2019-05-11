using System.Linq;
using System.Threading.Tasks;

namespace NewsFeeds.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetAsync(int id);
        IQueryable<TEntity> GetAll();        
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task<bool> ContainsEntityWithId(int id);
        void Delete(int id);
        void Delete(int id, int entityId);
    }
}