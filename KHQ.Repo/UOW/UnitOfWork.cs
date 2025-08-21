using KHQ.Repo.Data;
using KHQ.Repo.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace KHQ.Repo.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KHQDBContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(KHQDBContext context)
        {
            _context = context;
        }

        public IBaseRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new BaseRepository<T>(_context);
                _repositories[type] = repoInstance;
            }

            return (IBaseRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        private IDbContextTransaction? _transaction;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

    }

}
