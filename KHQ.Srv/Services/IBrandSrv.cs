using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IBrandSrv
    {
        Task<IEnumerable<BrandsVM>> GetAllAsync();
        Task<BrandsVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(BrandsVM entity);
        Task<int> UpdateAsync(BrandsVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
