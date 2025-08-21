using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface ISubProductSrv
    {
        Task<IEnumerable<SubProductVM>> GetAllAsync();
        Task<SubProductVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(SubProductVM entity);
        Task<int> UpdateAsync(SubProductVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
