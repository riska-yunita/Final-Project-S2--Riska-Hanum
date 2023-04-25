using ProjectClientServer.Repositories.Contract;
using ProjectClientServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ProjectClientServer.Repositories
{
    public class GeneralRepository<TEntity, TKey, TContext> : IGeneralRepository<TEntity, TKey>
        where TEntity : class
        where TContext : MyContext
    {
        protected TContext _context;
        public GeneralRepository(TContext context)
        {
            _context = context;
        }
        
        //old
        /*public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetById(TKey key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }

        public async Task<TEntity?> Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(TKey key)
        {
            var entity = _context.Set<TEntity>().Find(key);
            if (entity == null)
            {
                return 0;
            }
            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync();
        }*/
        //till this
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey key)
        {
            return await _context.Set<TEntity?>().FindAsync(key);
        }

        public virtual async Task<TEntity?> InsertAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TKey key)
        {
            var entity = await GetByIdAsync(key);
            _context.Set<TEntity>().Remove(entity!);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> IsExist(TKey key)
        {
            var entity = await GetByIdAsync(key);
            return entity != null;
        }
    }
}
