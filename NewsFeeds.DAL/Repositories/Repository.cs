using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsFeeds.DAL.EF;

namespace NewsFeeds.DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext DbContext;
        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public abstract Task<TEntity> GetAsync(int id);
        public abstract IQueryable<TEntity> GetAll();        
        public abstract Task AddAsync(TEntity entity);
        public abstract void Update(TEntity entity);
        public abstract Task<bool> ContainsEntityWithId(int id);
        public virtual void Delete(int id, int entityId) { }
        public abstract void Delete(int id);
    }
}
