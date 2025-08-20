using KHQ.Repo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Repo.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly KHQDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(KHQDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRange(List<T> entities)
        {
            try
            {

                await _dbSet.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task Delete(T entity)
        {
            //await _dbSet.ExecuteDeleteAsync<T>();
            _dbSet.Remove(entity);
        }

        public virtual IQueryable<T> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();

        }
    }

}
