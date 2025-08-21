using KHQ.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Srv.Services
{
    public interface ISocialMediaSrv
    {
        Task<IEnumerable<SocialMediaVM>> GetAllAsync();
        Task<SocialMediaVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(SocialMediaVM entity);
        Task<int> UpdateAsync(SocialMediaVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
