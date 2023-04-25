namespace ProjectClientServer.Repositories.Contract
{
    public interface IGeneralRepository<TEntity, TKey>
        where TEntity : class
        
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey key);
        Task<TEntity?> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TKey key);
        Task<bool> IsExist(TKey key);

    }
}
