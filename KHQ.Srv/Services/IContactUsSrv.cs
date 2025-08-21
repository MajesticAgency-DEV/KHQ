using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IContactUsSrv
    {
        Task<IEnumerable<ContactUsVM>> GetAllAsync();
        Task<ContactUsVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(ContactUsVM entity);
        Task<int> UpdateAsync(ContactUsVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
