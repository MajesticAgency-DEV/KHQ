using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly IAboutUsSrv _AboutUsSrv;
        private readonly IImageService _imageService;

        public AboutUsController(IAboutUsSrv aboutUsSrv, IImageService imageService)
        {
            _AboutUsSrv = aboutUsSrv;
            _imageService = imageService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<AboutUsVM> aboutUsData = await _AboutUsSrv.GetAllAsync();
            return View(aboutUsData);
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
