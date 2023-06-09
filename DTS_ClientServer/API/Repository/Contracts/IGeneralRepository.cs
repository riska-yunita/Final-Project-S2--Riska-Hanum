﻿namespace API.Repository.Contracts;

public interface IGeneralRepository<TEntity, TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey key);
    Task<TEntity?> InsertAsync (TEntity entity);
    Task<int> UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey key);
    Task<bool> IsExist(TKey key);
}
