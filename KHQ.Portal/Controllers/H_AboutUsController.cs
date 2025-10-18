using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.ViewModel;
using KHQ.Srv.Caching;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class H_AboutUsController : Controller
    {
        private readonly IH_AboutUsService _H_AboutUsService;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;


        public H_AboutUsController(IH_AboutUsService h_AboutUsService, IImageService imageService, ICacheService cacheService)
        {
            _H_AboutUsService = h_AboutUsService;
            _imageService = imageService;
            _cacheService = cacheService;
        }
        public async Task<IActionResult> Index()
        {
            var h_AboutUsData = await _H_AboutUsService.GetAllAsync();
            return View(h_AboutUsData);
        }
        public async Task<IActionResult> Images()
        {
            var images = await _imageService.GetImagesByImageTypeAsync(ImageType.AboutUs_Home);
            return View(images);
        }
        public async Task<IActionResult> GetById(Guid id)
        {
            var h_AboutUsData = await _H_AboutUsService.GetByIdAsync(id);
            return View(h_AboutUsData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(H_AboutUsVM h_AboutUsVM)
        {
            var result = await _H_AboutUsService.AddAsync(h_AboutUsVM);
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
        public async Task<IActionResult> Update(H_AboutUsVM h_AboutUsVM)
        {
            var result = await _H_AboutUsService.UpdateAsync(h_AboutUsVM);
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
            var result = await _H_AboutUsService.DeleteAsync(id);
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
    }
}
