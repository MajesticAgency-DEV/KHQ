using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class ContactUsSrv : IContactUsSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactUsSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(ContactUsVM entity)
        {
            var contactUsToBeAdded = _mapper.Map<ContactUs>(entity);
            await _unitOfWork.Repository<ContactUs>().AddAsync(contactUsToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var contactUsToBeDeleted = await _unitOfWork.Repository<ContactUs>().GetByIdAsync(id);
            if (contactUsToBeDeleted != null)
            {
                _unitOfWork.Repository<ContactUs>().Delete(contactUsToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<ContactUsVM>> GetAllAsync()
        {
            var h_AboutUs = await _unitOfWork.Repository<ContactUs>().GetAllAsync();
            return _mapper.Map<IEnumerable<ContactUsVM>>(h_AboutUs);
        }

        public async Task<ContactUsVM?> GetByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Repository<ContactUs>().GetByIdAsync(id);
            return _mapper.Map<ContactUsVM>(category);
        }

        public async Task<int> UpdateAsync(ContactUsVM entity)
        {
            var contactUsToBeUpdated = _mapper.Map<ContactUs>(entity);
            _unitOfWork.Repository<ContactUs>().Update(contactUsToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
