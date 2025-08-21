using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaSrv _SocialMediaSrv;
        private readonly IImageService _imageService;

        public SocialMediaController(ISocialMediaSrv socialMediaSrv, IImageService imageService)
        {
            _SocialMediaSrv = socialMediaSrv;
            _imageService = imageService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SocialMediaVM> socialMedia = await _SocialMediaSrv.GetAllAsync();            
            return View(socialMedia);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var socialMediaData = await _SocialMediaSrv.GetByIdAsync(id);
            return View(socialMediaData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SocialMediaVM socialMedia)
        {
            var result = await _SocialMediaSrv.AddAsync(socialMedia);
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
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(SocialMediaVM socialMedia)
        {
            var result = await _SocialMediaSrv.UpdateAsync(socialMedia);
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
            var result = await _SocialMediaSrv.DeleteAsync(id);
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
