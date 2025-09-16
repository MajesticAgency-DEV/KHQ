using KHQ.Domain.DTOs;
using KHQ.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Srv.Services
{
    public interface IEmailsSrv
    {
        Task<IEnumerable<EmailsVM>> GetAllAsync();
        Task<EmailsVM?> GetByIdAsync(Guid id);
        Task<int> AddAsync(EmailsDto entity);
        Task<int> UpdateAsync(EmailsVM entity);
        Task<int> DeleteAsync(Guid id);
    }
}
