using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IStainDetailsSrv
    {
        Task<IEnumerable<StainDetailsVM>> GetAllAsync();
        Task<StainDetailsVM?> GetByIdAsync(Guid id);
        Task<StainDetailsVM?> GetByStainId(Guid stainId);
        Task<int> AddAsync(StainDetailsVM entity);
        Task<int> UpdateAsync(StainDetailsVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
