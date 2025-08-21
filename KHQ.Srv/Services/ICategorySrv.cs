using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface ICategorySrv
    {
        Task<IEnumerable<CategoryVM>> GetAllAsync();
        Task<CategoryVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(CategoryVM entity);
        Task<int> UpdateAsync(CategoryVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
