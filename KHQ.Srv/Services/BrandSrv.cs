using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class BrandSrv : IBrandSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(BrandsVM entity)
        {
            try
            {
                var brandToBeAdded = _mapper.Map<Brands>(entity);
                await _unitOfWork.Repository<Brands>().AddAsync(brandToBeAdded);
                var result = await _unitOfWork.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var brandToBeDeleted = await _unitOfWork.Repository<Brands>().GetByIdAsync(id);
            if (brandToBeDeleted != null)
            {
                _unitOfWork.Repository<Brands>().Delete(brandToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<BrandsVM>> GetAllAsync()
        {
            var h_AboutUs = await _unitOfWork.Repository<Brands>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandsVM>>(h_AboutUs);
        }

        public async Task<BrandsVM?> GetByIdAsync(Guid id)
        {
            var brand = await _unitOfWork.Repository<Brands>().GetByIdAsync(id);
            return _mapper.Map<BrandsVM>(brand);
        }

        public async Task<int> UpdateAsync(BrandsVM entity)
        {
            var brandToBeUpdated = _mapper.Map<Brands>(entity);
            _unitOfWork.Repository<Brands>().Update(brandToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}

