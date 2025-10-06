using AutoMapper;
using KHQ.Caching;
using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IProductSrv _productSrv;
        private readonly IMapper _mapper;
        //private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService/*, IProductSrv productSrv, IImageService imageService*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
            //_productSrv = productSrv;
            //_imageService = imageService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ProductDtoNew> GetAll()
        {
            //var lang = HttpContext.Request.Headers["Accept-Language"].FirstOrDefault()?.ToLower() ?? "en";
            var products = await _unitOfWork.Repository<Product>().Queryable()
        .Include(p => p.SubProducts)
        .ToListAsync();

            var categoryIds = products.Select(p => p.CategoryId).Distinct().ToList();
            var brandIds = products.Select(p => p.BrandId).Distinct().ToList();

            var categories = await _unitOfWork.Repository<Category>().Queryable()
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();

            var brands = await _unitOfWork.Repository<Brands>().Queryable()
                .Where(b => brandIds.Contains(b.Id))
                .ToListAsync();

            var result = _mapper.Map<List<ProductDto>>(products);

            // Attach Category & Brand Names + Images
            foreach (var product in result)
            {
                var category = categories.FirstOrDefault(c => c.Id == products.First(p => p.Id == product.Id).CategoryId);
                var brand = brands.FirstOrDefault(b => b.Id == products.First(p => p.Id == product.Id).BrandId);

                product.CategoryName = category != null
                    ? (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? category.NameAr : category.NameEn)
                    : string.Empty;

                product.BrandName = brand != null
                    ? (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? brand.NameAr : brand.NameEn)
                    : string.Empty;

                foreach (var sub in product.SubProducts)
                {
                    var image = await _unitOfWork.Repository<Image>().Queryable()
                        .Where(i => i.F_Key == product.Id)
                        .OrderBy(i => i.Sort)
                        .FirstOrDefaultAsync();

                    sub.ImageUrl = image?.PathLink;
                }
            }
            ProductDtoNew productDtoNew = new ProductDtoNew();
            productDtoNew.Products = result;

            var coverPhoto = await _unitOfWork.Repository<Image>().Queryable()
                        .Where(x => x.ImageType == ImageType.Product_Cover)
                        .FirstOrDefaultAsync();
            
            productDtoNew.CoverPhoto = coverPhoto != null ? coverPhoto.PathLink : "";

            return productDtoNew;
        }
        
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // 1️⃣ Get language from header
            var lang = HttpContext.Request.Headers["Accept-Language"].FirstOrDefault()?.ToLower() ?? "en";

            // 2️⃣ Load the product with its subproducts (no tracking for better performance)
            var product = await _unitOfWork.Repository<Product>().Queryable()
                .AsNoTracking()
                .Include(p => p.SubProducts)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            // 3️⃣ Get category and brand (only one each)
            var category = await _unitOfWork.Repository<Category>().Queryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == product.CategoryId);

            var brand = await _unitOfWork.Repository<Brands>().Queryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == product.BrandId);

            // 4️⃣ Get all images for this product (F_Key = product.Id)
            var images = await _unitOfWork.Repository<Image>().Queryable()
                .AsNoTracking()
                .Where(i => i.F_Key == product.Id)
                .OrderBy(i => i.Sort)
                .ToListAsync();

            // 5️⃣ Sort sub-products and assign images based on Sort/SortOrder
            var subProducts = product.SubProducts.OrderBy(sp => sp.SortOrder).ToList();
            for (int i = 0; i < subProducts.Count; i++)
            {
                // Assign image sequentially by index
                if (i < images.Count)
                {
                    // We temporarily attach image path to avoid another query
                    subProducts[i].Product = null; // prevent circular ref
                }
            }

            // 6️⃣ Map to DTO (with language context)
            var productDto = _mapper.Map<ProductDto>(product, opt => opt.Items["lang"] = lang);

            // 7️⃣ Attach CategoryName / BrandName
            productDto.CategoryName = category != null
                ? (lang == "ar" ? category.NameAr : category.NameEn)
                : string.Empty;

            productDto.BrandName = brand != null
                ? (lang == "ar" ? brand.NameAr : brand.NameEn)
                : string.Empty;

            // 8️⃣ Attach image URLs in sorted order
            var orderedSubDtos = productDto.SubProducts.OrderBy(sp => sp.SortOrder).ToList();
            for (int i = 0; i < orderedSubDtos.Count; i++)
            {
                if (i < images.Count)
                    orderedSubDtos[i].ImageUrl = images[i].PathLink;
            }

            productDto.SubProducts = orderedSubDtos.Where(x => x.ImageUrl != null || x.ImageUrl == string.Empty).ToList();

            return Ok(productDto);
        }

        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(Guid categoryId)
        {
            var lang = HttpContext.Request.Headers["Accept-Language"].FirstOrDefault()?.ToLower();

            var products = await _unitOfWork.Repository<Product>().Queryable()
                .AsNoTracking()
                .Include(p => p.SubProducts)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            if (!products.Any())
                return Ok(new List<ProductDto>());

            // Collect all brandIds for lookup
            var brandIds = products.Select(p => p.BrandId).Distinct().ToList();

            var categories = await _unitOfWork.Repository<Category>().Queryable()
                .AsNoTracking()
                .Where(c => c.Id == categoryId)
                .ToListAsync();

            var brands = await _unitOfWork.Repository<Brands>().Queryable()
                .AsNoTracking()
                .Where(b => brandIds.Contains(b.Id))
                .ToListAsync();

            // Collect subproduct IDs to get images
            var subProductIds = products.Select(sp => sp.Id).ToList();

            var images = await _unitOfWork.Repository<Image>().Queryable()
                .AsNoTracking()
                .Where(i => subProductIds.Contains(i.F_Key))
                .GroupBy(i => i.F_Key)
                .Select(g => g.OrderBy(i => i.Sort).FirstOrDefault())
                .ToListAsync();

            var result = _mapper.Map<List<ProductDto>>(products, opt => opt.Items["lang"] = lang);

            foreach (var productDto in result)
            {
                var entity = products.First(p => p.Id == productDto.Id);
                var category = categories.FirstOrDefault(c => c.Id == entity.CategoryId);
                var brand = brands.FirstOrDefault(b => b.Id == entity.BrandId);

                productDto.CategoryName = category != null
                    ? (lang == "ar" ? category.NameAr : category.NameEn)
                    : string.Empty;

                productDto.BrandName = brand != null
                    ? (lang == "ar" ? brand.NameAr : brand.NameEn)
                    : string.Empty;

                foreach (var sub in productDto.SubProducts)
                {
                    var img = images.FirstOrDefault(i => i.F_Key == productDto.Id);
                    sub.ImageUrl = img?.PathLink;
                }
            }

            return Ok(result);
        }


        [HttpGet("GetByBrand/{brandId}")]
        public async Task<IActionResult> GetByBrandId(Guid brandId)
        {
            var lang = HttpContext.Request.Headers["Accept-Language"].FirstOrDefault()?.ToLower() ?? "en";

            var products = await _unitOfWork.Repository<Product>().Queryable()
                .AsNoTracking()
                .Include(p => p.SubProducts)
                .Where(p => p.BrandId == brandId)
                .ToListAsync();

            if (!products.Any())
                return Ok(new List<ProductDto>());

            // Collect all categoryIds for lookup
            var categoryIds = products.Select(p => p.CategoryId).Distinct().ToList();

            var categories = await _unitOfWork.Repository<Category>().Queryable()
                .AsNoTracking()
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();

            var brands = await _unitOfWork.Repository<Brands>().Queryable()
                .AsNoTracking()
                .Where(b => b.Id == brandId)
                .ToListAsync();

            // Collect subproduct IDs to get images
            var subProductIds = products.Select(sp => sp.Id).ToList();

            var images = await _unitOfWork.Repository<Image>().Queryable()
                .AsNoTracking()
                .Where(i => subProductIds.Contains(i.F_Key))
                .GroupBy(i => i.F_Key)
                .Select(g => g.OrderBy(i => i.Sort).FirstOrDefault())
                .ToListAsync();

            var result = _mapper.Map<List<ProductDto>>(products, opt => opt.Items["lang"] = lang);

            foreach (var productDto in result)
            {
                var entity = products.First(p => p.Id == productDto.Id);
                var category = categories.FirstOrDefault(c => c.Id == entity.CategoryId);
                var brand = brands.FirstOrDefault(b => b.Id == entity.BrandId);

                productDto.CategoryName = category != null
                    ? (lang == "ar" ? category.NameAr : category.NameEn)
                    : string.Empty;

                productDto.BrandName = brand != null
                    ? (lang == "ar" ? brand.NameAr : brand.NameEn)
                    : string.Empty;

                foreach (var sub in productDto.SubProducts)
                {
                    var img = images.FirstOrDefault(i => i.F_Key == productDto.Id);
                    sub.ImageUrl = img?.PathLink;
                }
            }

            return Ok(result);
        }


    }
}
