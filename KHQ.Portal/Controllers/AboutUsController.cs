using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Srv.Caching;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly IAboutUsSrv _AboutUsSrv;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;

        public AboutUsController(IAboutUsSrv aboutUsSrv, IImageService imageService,ICacheService cacheService)
        {
            _AboutUsSrv = aboutUsSrv;
            _imageService = imageService;
            _cacheService = cacheService;
        }
        public async Task<IActionResult> Index()
        {
            List<AboutUsVM> aboutUsData = _AboutUsSrv.GetAllAsync().Result.ToList();
            var aboutUsImages = await _imageService.GetImagesByImageTypeAsync(ImageType.AboutUs_Page);

            // Assign points and images sequentially
            for (int i = 0; i < aboutUsData.Count(); i++)
            {
                var item = aboutUsData[i];

                // Assign image sequentially
                if (i < aboutUsImages.Count)
                    item.ImageLink = aboutUsImages[i].PathLink;
                else
                    item.ImageLink = string.Empty;
            }
            return View(aboutUsData);
        }
        public async Task<IActionResult> CoverSection()
        {
            var images = await _imageService.GetImagesByImageTypeAsync(ImageType.AboutUs_Cover);
            return View(images);
        }
        public async Task<IActionResult> ImagesSection()
        {
            var images = await _imageService.GetImagesByImageTypeAsync(ImageType.AboutUs_Page);
            return View(images);
        }
        public async Task<IActionResult> GetById(Guid id)
        {
            var aboutUsData = await _AboutUsSrv.GetByIdAsync(id);
            return View(aboutUsData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AboutUsVM aboutUs)
        {
            var result = await _AboutUsSrv.AddAsync(aboutUs);
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
        [HttpPost]
        public async Task<IActionResult> Update(AboutUsVM aboutUs)
        {
            var result = await _AboutUsSrv.UpdateAsync(aboutUs);
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
            var result = await _AboutUsSrv.DeleteAsync(id);
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
