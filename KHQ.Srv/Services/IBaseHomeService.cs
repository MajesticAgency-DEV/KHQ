using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;

namespace KHQ.Srv.Services
{
    public interface IBaseHomeService
    {
        // Get all BaseHome records as DTOs
        Task<IEnumerable<BaseHomeVM>> GetAllAsync();

        // Get a single record by ID
        Task<BaseHomeVM?> GetByIdAsync(Guid id);

        // Add a new record
        Task<int> AddAsync(BaseHomeVM dto);

        // Update an existing record
        Task<int> UpdateAsync(BaseHomeVM dto);

        // Delete by ID
        Task<int> DeleteAsync(Guid id);

        // Advanced: Expose IQueryable for filtering/search (returns entity, not DTO)
        IQueryable<BaseHome> Queryable();

        // Get section data (returns the single BaseHome record)
        Task<BaseHomeVM> GetSectionDataAsync(int sectionType);
        // Save/Update section data
        Task<int> SaveSectionAsync(BaseHomeVM request);


    }


}
