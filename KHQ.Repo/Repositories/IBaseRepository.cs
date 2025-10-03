using System.Linq.Expressions;

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
        Task DeleteRange(List<T> entities);
        IQueryable<T> Queryable(); // For LINQ support
        Task<int> SaveChangesAsync();
    }
}
