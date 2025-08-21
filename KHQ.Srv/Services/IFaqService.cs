using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IFaqService
    {
        Task<IEnumerable<FaqVM>> GetAllAsync();
        Task<FaqVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(FaqVM entity);
        Task<int> UpdateAsync(FaqVM entity);
        Task<int> DeleteAsync(Guid id);
    }

}
