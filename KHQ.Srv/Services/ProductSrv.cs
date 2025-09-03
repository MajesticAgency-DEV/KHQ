using AutoMapper;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;
using Microsoft.EntityFrameworkCore;

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
            foreach (var item in productToBeAdded.SubProducts)
            {
                item.ProductId = productToBeAdded.Id;
            }
            await _unitOfWork.Repository<SubProduct>().AddRange(productToBeAdded.SubProducts);
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
            var products = await _unitOfWork.Repository<Product>().Queryable()
                    .Include(p => p.SubProducts)
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        NameEn = p.NameEn,
                        NameAr = p.NameAr,
                        DescriptionEn = p.DescriptionEn,
                        DescriptionAr = p.DescriptionAr,
                        Price = p.Price,
                        SubProducts = p.SubProducts.Select(sp => new SubProduct
                        {
                            Id = sp.Id,
                            DescriptionEn = sp.DescriptionEn,
                            DescriptionAr = sp.DescriptionAr
                            // 👈 Notice: we don’t include `Product` here!
                        }).ToList()
                    })
                    .ToListAsync();
            return _mapper.Map<IEnumerable<ProductVM>>(products);
        }

        public async Task<ProductVM?> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Repository<Product>()
                .Queryable()
                .Include(p => p.SubProducts)
                .FirstOrDefaultAsync(p => p.Id == id);

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
