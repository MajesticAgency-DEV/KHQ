using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Srv.Services
{
    public interface IEmailSettingsSRV
    {
        Task<IEnumerable<EmailSettingsVM>> GetAllAsync();
        Task<EmailSettingsVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(EmailSettingsVM entity);
        Task<int> UpdateAsync(EmailSettingsVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
