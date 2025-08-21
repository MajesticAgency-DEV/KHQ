using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class FaqService : IFaqService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FaqService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FaqVM>> GetAllAsync()
        {
            var fAQs = await _unitOfWork.Repository<FAQ>().GetAllAsync();
            return _mapper.Map<IEnumerable<FaqVM>>(fAQs);
        }

        public async Task<FaqVM?> GetByIdAsync(Guid id)
        {
            var fAQ = await _unitOfWork.Repository<FAQ>().GetByIdAsync(id);
            return _mapper.Map<FaqVM>(fAQ);
        }

        public async Task<int> AddAsync(FaqVM entity)
        {
            var fAQToBeAdded = _mapper.Map<FAQ>(entity);
            await _unitOfWork.Repository<FAQ>().AddAsync(fAQToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(FaqVM entity)
        {
            var fAQToBeUpdated = _mapper.Map<FAQ>(entity);
            _unitOfWork.Repository<FAQ>().Update(fAQToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var fAQToBeDeleted = await _unitOfWork.Repository<FAQ>().GetByIdAsync(id);
            if (fAQToBeDeleted != null)
            {
                _unitOfWork.Repository<FAQ>().Delete(fAQToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}
