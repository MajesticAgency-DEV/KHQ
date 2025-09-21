using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Srv.Services
{
    public interface IWhyChooseUsSrv
    {
        Task<IEnumerable<WhyChooseUsVM>> GetAllAsync();
        Task<WhyChooseUsVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(WhyChooseUsVM entity);
        Task<int> UpdateAsync(WhyChooseUsVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
