namespace ProjectAPI.DataAccessLayer.Repositories
{
    public interface IRepository<TEntity , Tkey> where TEntity : class
    {
        Task<TEntity?> GetAsync(Tkey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> GetQueryable(); 
    }
}
