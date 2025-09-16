using KHQ.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Srv.Services
{
    public interface IBrouchuresSrv
    {
        Task<IEnumerable<BrouchuresVM>> GetAllAsync();
        Task<BrouchuresVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(BrouchuresVM entity);
        Task<int> UpdateAsync(BrouchuresVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
