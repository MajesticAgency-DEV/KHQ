using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class SubProductSrv : ISubProductSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SubProductSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(SubProductVM entity)
        {
            var subProductToBeAdded = _mapper.Map<SubProduct>(entity);
            await _unitOfWork.Repository<SubProduct>().AddAsync(subProductToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var productToBeDeleted = await _unitOfWork.Repository<SubProduct>().GetByIdAsync(id);
            if (productToBeDeleted != null)
            {
                _unitOfWork.Repository<SubProduct>().Delete(productToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<SubProductVM>> GetAllAsync()
        {
            var subProducts = await _unitOfWork.Repository<SubProduct>().GetAllAsync();
            return _mapper.Map<IEnumerable<SubProductVM>>(subProducts);
        }

        public async Task<SubProductVM?> GetByIdAsync(Guid id)
        {
            var subProduct = await _unitOfWork.Repository<SubProduct>().GetByIdAsync(id);
            return _mapper.Map<SubProductVM>(subProduct);
        }

        public async Task<int> UpdateAsync(SubProductVM entity)
        {
            var subProductToBeUpdated = _mapper.Map<SubProduct>(entity);
            _unitOfWork.Repository<SubProduct>().Update(subProductToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
