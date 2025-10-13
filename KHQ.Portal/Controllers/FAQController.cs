using KHQ.Domain;
using KHQ.Domain.ViewModel;
using KHQ.Srv.Caching;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class FAQController : Controller
    {
        private readonly IFaqService _FaqService;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;

        public FAQController(IFaqService faqService, IImageService imageService,ICacheService cacheService)
        {
            _FaqService = faqService;
            _imageService = imageService;
            _cacheService = cacheService;
        }
        public async Task<IActionResult> Index()
        {
            var fAQData = await _FaqService.GetAllAsync();
            return View(fAQData);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var fAQData = await _FaqService.GetByIdAsync(id);
            return View(fAQData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(FaqVM faqVM)
        {
            var result = await _FaqService.AddAsync(faqVM);
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
        public async Task<IActionResult> Update(FaqVM faqVM)
        {
            var result = await _FaqService.UpdateAsync(faqVM);
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
            var result = await _FaqService.DeleteAsync(id);
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
        public async Task<IActionResult> CoverSection()
        {
            var images = await _imageService.GetImagesByImageTypeAsync(ImageType.FAQ_Cover);
            return View(images);
        }
    }
}
