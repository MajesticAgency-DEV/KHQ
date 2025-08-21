using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IProductSrv
    {
        Task<IEnumerable<ProductVM>> GetAllAsync();
        Task<ProductVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(ProductVM entity);
        Task<int> UpdateAsync(ProductVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
