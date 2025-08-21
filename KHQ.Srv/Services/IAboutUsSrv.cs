using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IAboutUsSrv
    {
        Task<IEnumerable<AboutUsVM>> GetAllAsync();
        Task<AboutUsVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(AboutUsVM entity);
        Task<int> UpdateAsync(AboutUsVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
