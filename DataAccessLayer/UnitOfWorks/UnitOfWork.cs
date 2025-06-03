
using ProjectAPI.DataAccessLayer.Data.Context;
using ProjectAPI.DataAccessLayer.Repositories;
using System.Collections.Concurrent;

namespace ProjectAPI.DataAccessLayer.UnitOfWorks

{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly ConcurrentDictionary<string, object> _repository;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repository = new();
        }

        public async Task<int> SaveCheangesAsync() => await _storeContext.SaveChangesAsync();
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class=>(IRepository<TEntity , TKey>) _repository.GetOrAdd(typeof(TEntity).Name, _=> new Repository<TEntity , TKey>(_storeContext));
    }
}
