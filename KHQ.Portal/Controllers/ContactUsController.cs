using KHQ.Domain;
using KHQ.Domain.ViewModel;
using KHQ.Portal.Service;
using KHQ.Srv.Caching;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUsSrv _ContactUsSrv;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;

        public ContactUsController(IContactUsSrv contactUsSrv, IImageService imageService, ICacheService cacheService)
        {
            _ContactUsSrv = contactUsSrv;
            _imageService = imageService;
            _cacheService = cacheService;
        }
        public async Task<IActionResult> Index()
        {
            var contactUsData = await _ContactUsSrv.GetAllAsync();
            return View(contactUsData);
        }
        public async Task<IActionResult> CoverSection()
        {
            var images = await _imageService.GetImagesByImageTypeAsync(ImageType.ContactUs_Cover);
            return View(images);
        }
        public async Task<IActionResult> GetById(Guid id)
        {
            var contactUsData = await _ContactUsSrv.GetByIdAsync(id);
            return View(contactUsData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ContactUsVM contactUsVM)
        {
            var result = await _ContactUsSrv.AddAsync(contactUsVM);
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
        public async Task<IActionResult> Update(ContactUsVM contactUsVM)
        {
            var result = await _ContactUsSrv.UpdateAsync(contactUsVM);
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
            var result = await _ContactUsSrv.DeleteAsync(id);
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
