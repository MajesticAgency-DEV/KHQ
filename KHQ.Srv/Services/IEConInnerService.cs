using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IEConInnerService
    {
        Task<IEnumerable<E_Con_InnerVM>> GetAllAsync();
        Task<E_Con_InnerVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(E_Con_InnerVM entity);
        Task<int> UpdateAsync(E_Con_InnerVM entity);
        Task<int> DeleteAsync(Guid id);
    }

}
