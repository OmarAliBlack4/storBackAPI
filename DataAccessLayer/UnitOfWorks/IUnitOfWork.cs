using ProjectAPI.DataAccessLayer.Repositories;

namespace ProjectAPI.DataAccessLayer.UnitOfWorks

{
    public interface IUnitOfWork
    {
        public Task<int> SaveCheangesAsync();

        IRepository<TEntity , TKey> GetRepository<TEntity , TKey>() where TEntity : class;
    }
}
