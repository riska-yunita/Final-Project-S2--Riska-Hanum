using API.Repository.Contracts;
using API.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class GeneralRepository<TEntity, TKey, TContext> : IGeneralRepository<TEntity, TKey>
    where TEntity : class
    where TContext : MyContext
{
    protected TContext _context;
    private readonly DbSet<TEntity> entities;
    public GeneralRepository(TContext context)
    {
        _context = context;
        entities = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await entities.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(TKey key)
    {
        return await entities.FindAsync(key);
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        entities.Update(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey key)
    {
        var entity = await GetByIdAsync(key);
        entities.Remove(entity);
        await _context.SaveChangesAsync(); 
    }
    
    public virtual async Task<bool> IsExist(TKey key)
    {
        var entity = await GetByIdAsync(key);
        return entity !=null;
    }

    public virtual async Task<TEntity?> InsertAsync(TEntity entity)
    {
        entities.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
