using KHQ.Domain;
using KHQ.Domain.ViewModel;
using KHQ.Srv.Caching;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandSrv _BrandSrv;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;

        public BrandsController(IBrandSrv brandSrv, IImageService imageService,ICacheService cacheService)
        {
            _BrandSrv = brandSrv;
            _imageService = imageService;
            _cacheService = cacheService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<BrandsVM> brandsData = await _BrandSrv.GetAllAsync();

            foreach (BrandsVM brandsVM in brandsData)
            {
                // Initialize PathLink if it's null
                if (brandsVM.ImageLink == null)
                {
                    brandsVM.ImageLink = "";
                }
                var images = await _imageService.GetImagesAsync(brandsVM.Id, ImageType.Brands);

                foreach (var image in images)
                {
                    brandsVM.ImageLink = image.PathLink;
                }
            }
            brandsData = brandsData.OrderBy(x => x.NameEn).ToList();
            return View(brandsData);
        }

        public async Task<IActionResult> CoverSection()
        {
            var images = await _imageService.GetImagesByImageTypeAsync(ImageType.Brands_Cover);
            return View(images);
        }
        public async Task<IActionResult> GetById(Guid id)
        {
            var brandsData = await _BrandSrv.GetByIdAsync(id);
            return View(brandsData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(BrandsVM brandsVM)
        {
            var result = await _BrandSrv.AddAsync(brandsVM);
            if (result > 0)
            {
                _cacheService.ClearAll();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(BrandsVM brandsVM)
        {
            var result = await _BrandSrv.UpdateAsync(brandsVM);
            if (result > 0)
            {
                _cacheService.ClearAll();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _BrandSrv.DeleteAsync(id);
            if (result > 0)
            {
                var res = await _imageService.DeleteImagesAsync(id);
                if (res > 0)
                {
                    _cacheService.ClearAll();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
