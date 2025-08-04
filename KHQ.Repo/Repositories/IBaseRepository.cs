using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Repo.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddRange(List<T> entities);
        void Update(T entity);
        Task Delete(T entity);
        IQueryable<T> Queryable(); // For LINQ support
        Task<int> SaveChangesAsync();
    }
}
