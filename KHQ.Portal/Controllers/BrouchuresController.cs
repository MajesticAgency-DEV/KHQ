using KHQ.Domain;
using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class BrouchuresController : Controller
    {
        private readonly IBrouchuresSrv _BrouchuresSrv;
        private readonly IImageService _imageService;

        public BrouchuresController(IBrouchuresSrv brouchuresSrv, IImageService imageService)
        {
            _imageService = imageService;
            _BrouchuresSrv = brouchuresSrv;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<BrouchuresVM> brouchuresData = await _BrouchuresSrv.GetAllAsync();

            return View(brouchuresData);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var brouchuresData = await _BrouchuresSrv.GetByIdAsync(id);
            return View(brouchuresData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(BrouchuresVM brouchuresVM)
        {
            var result = await _BrouchuresSrv.AddAsync(brouchuresVM);
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
        public async Task<IActionResult> Update(BrouchuresVM brouchuresVM)
        {
            var result = await _BrouchuresSrv.UpdateAsync(brouchuresVM);
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
            var result = await _BrouchuresSrv.DeleteAsync(id);
            if (result > 0)
            {
                var res = await _imageService.DeleteImagesAsync(id);
                if (res > 0)
                {
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
