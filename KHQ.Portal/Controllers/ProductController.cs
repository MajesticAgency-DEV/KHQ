using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.ViewModel;
using KHQ.Portal.Service;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductSrv _ProductSrv;
        private readonly ICategorySrv _CategorySrv;
        private readonly IImageService _imageService;


        public ProductController(IProductSrv productSrv, ICategorySrv categorySrv, IImageService imageService)
        {
            _ProductSrv = productSrv;
            _CategorySrv = categorySrv;
            _imageService = imageService;
        }
        public async Task<IActionResult> Index(string search = "", string sortBy = "", int page = 1, int pageSize = 8)
        {
            var products = await _ProductSrv.GetAllAsync();

            foreach (ProductVM pro in products)
            {
                // Initialize PathLink if it's null
                if (pro.PathLink == null)
                {
                    pro.PathLink = new List<string>();
                }

                var images = await _imageService.GetImagesAsync(pro.Id, ImageType.Product);

                foreach (var image in images.OrderBy(x => x.Sort))
                {
                    pro.PathLink.Add(image.PathLink);
                }
            }

            // 🔍 Search filter
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p =>
                    p.NameEn.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.NameAr.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // ↕️ Sorting
            // ↕️ Sorting
            products = sortBy switch
            {
                "PriceLow" => products.OrderBy(p => p.Price).ToList(),
                "PriceHigh" => products.OrderByDescending(p => p.Price).ToList(),
                "AToZ" => products.OrderBy(p => p.NameEn).ToList(),       
                "ZToA" => products.OrderByDescending(p => p.NameEn).ToList(),
                _ => products
            };

            // 📄 Pagination
            int totalItems = products.Count();
            var pagedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Pass paging info to view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;

            return View(pagedProducts);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var productData = await _ProductSrv.GetByIdAsync(id);
            return View(productData);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IEnumerable<CategoryVM>> GetAllCategories()
        {
            var categories = await _CategorySrv.GetAllAsync();
            return categories;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductVM productVM)
        {
            var result = await _ProductSrv.AddAsync(productVM);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var productData = await _ProductSrv.GetByIdAsync(id);

            if (productData != null)
            {
                var images = await _imageService.GetImagesAsync(productData.Id, ImageType.Product);
                var orderedImages = images.OrderBy(x => x.Sort).ToList();

                productData.PathLink = orderedImages.Select(x => x.PathLink).ToList();

                // Assign one image per subproduct by index
                for (int i = 0; i < productData.SubProducts.Count; i++)
                {
                    var sub = productData.SubProducts[i];
                    sub.ImageUrl = i < orderedImages.Count
                        ? orderedImages[i].PathLink
                        : orderedImages.LastOrDefault()?.PathLink; // fallback
                }
            }

            return View(productData);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ProductVM productVM)
        {
            var result = await _ProductSrv.UpdateAsync(productVM);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _ProductSrv.DeleteAsync(id);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
