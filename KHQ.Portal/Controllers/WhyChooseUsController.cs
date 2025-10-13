using KHQ.Domain.ViewModel;
using KHQ.Srv.Caching;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KHQ.Portal.Controllers
{
    public class WhyChooseUsController : Controller
    {
        private readonly IWhyChooseUsSrv _whyChooseUsSrv;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;

        public WhyChooseUsController(IWhyChooseUsSrv whyChooseUsSrv, IImageService imageService, ICacheService cacheService)
        {
            _whyChooseUsSrv = whyChooseUsSrv;
            _imageService = imageService;
            _cacheService = cacheService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _whyChooseUsSrv.GetAllAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] WhyChooseUsVM whyChooseUs)
        {
            try
            {
                var result = await _whyChooseUsSrv.AddAsync(whyChooseUs);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] WhyChooseUsVM whyChooseUs)
        {
            try
            {
                var result = await _whyChooseUsSrv.UpdateAsync(whyChooseUs);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _whyChooseUsSrv.DeleteAsync(id);
                if (result > 0)
                {
                    _cacheService.ClearAll();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
