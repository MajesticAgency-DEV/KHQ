using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class H_AboutUsService : IH_AboutUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public H_AboutUsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(H_AboutUsVM entity)
        {
            var h_AboutUsToBeAdded = _mapper.Map<H_AboutUs>(entity);
            await _unitOfWork.Repository<H_AboutUs>().AddAsync(h_AboutUsToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var h_AboutUsToBeDeleted = await _unitOfWork.Repository<H_AboutUs>().GetByIdAsync(id);
            if (h_AboutUsToBeDeleted != null)
            {
                _unitOfWork.Repository<H_AboutUs>().Delete(h_AboutUsToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<H_AboutUsVM>> GetAllAsync()
        {
            var h_AboutUs = await _unitOfWork.Repository<H_AboutUs>().GetAllAsync();
            return _mapper.Map<IEnumerable<H_AboutUsVM>>(h_AboutUs);
        }

        public async Task<H_AboutUsVM?> GetByIdAsync(Guid id)
        {
            var h_AboutUs = await _unitOfWork.Repository<H_AboutUs>().GetByIdAsync(id);
            return _mapper.Map<H_AboutUsVM>(h_AboutUs);
        }

        public async Task<int> UpdateAsync(H_AboutUsVM entity)
        {
            var h_AboutUsToBeUpdated = _mapper.Map<H_AboutUs>(entity);
            _unitOfWork.Repository<H_AboutUs>().Update(h_AboutUsToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
