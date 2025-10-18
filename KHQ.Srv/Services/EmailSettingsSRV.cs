using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class EmailSettingsSRV : IEmailSettingsSRV
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmailSettingsSRV(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(EmailSettingsVM entity)
        {

            var emailSettingsToBeAdded = _mapper.Map<EmailSettings>(entity);
            await _unitOfWork.Repository<EmailSettings>().AddAsync(emailSettingsToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var emailSettingsToBeDeleted = await _unitOfWork.Repository<EmailSettings>().GetByIdAsync(id);
            if (emailSettingsToBeDeleted != null)
            {
                await _unitOfWork.Repository<EmailSettings>().Delete(emailSettingsToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<EmailSettingsVM>> GetAllAsync()
        {
            var emailSttingsData = await _unitOfWork.Repository<EmailSettings>().GetAllAsync();
            return _mapper.Map<IEnumerable<EmailSettingsVM>>(emailSttingsData);
        }

        public async Task<EmailSettingsVM?> GetByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Repository<EmailSettings>().GetByIdAsync(id);
            return _mapper.Map<EmailSettingsVM>(category);
        }

        public async Task<int> UpdateAsync(EmailSettingsVM entity)
        {
            var emailSettingToBeUpdated = _mapper.Map<EmailSettings>(entity);
            _unitOfWork.Repository<EmailSettings>().Update(emailSettingToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
