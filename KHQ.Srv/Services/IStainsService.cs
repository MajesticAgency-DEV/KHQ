using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IStainsService
    {
        Task<IEnumerable<StainsVM>> GetAllAsync();
        Task<StainsVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(StainsVM entity);
        Task<int> UpdateAsync(StainsVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
