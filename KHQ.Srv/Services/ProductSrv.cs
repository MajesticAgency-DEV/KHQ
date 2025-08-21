using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class ProductSrv : IProductSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(ProductVM entity)
        {
            var productToBeAdded = _mapper.Map<Product>(entity);
            await _unitOfWork.Repository<Product>().AddAsync(productToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var productToBeDeleted = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (productToBeDeleted != null)
            {
                _unitOfWork.Repository<Product>().Delete(productToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }

        public async Task<ProductVM?> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            return _mapper.Map<ProductVM>(product);
        }

        public async Task<int> UpdateAsync(ProductVM entity)
        {
            var productToBeUpdated = _mapper.Map<Product>(entity);
            _unitOfWork.Repository<Product>().Update(productToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
