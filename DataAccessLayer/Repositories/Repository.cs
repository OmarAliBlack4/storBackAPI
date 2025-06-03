
using Microsoft.EntityFrameworkCore;
using ProjectAPI.DataAccessLayer.Data.Context;

namespace ProjectAPI.DataAccessLayer.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly StoreContext _storeContext;

        public Repository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task AddAsync(TEntity entity) => await _storeContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _storeContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _storeContext.Set<TEntity>().Update(entity);
        public async Task<TEntity?> GetAsync(TKey id) => await _storeContext.Set<TEntity>().FindAsync(id);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _storeContext.Set<TEntity>().ToListAsync();

        public IQueryable<TEntity> GetQueryable() => _storeContext.Set<TEntity>();
    }

}
