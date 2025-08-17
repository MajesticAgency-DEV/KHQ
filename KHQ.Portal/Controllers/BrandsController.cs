using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandSrv _BrandSrv;

        public BrandsController(IBrandSrv brandSrv)
        {
            _BrandSrv = brandSrv;
        }
        public async Task<IActionResult> Index()
        {
            var brandsData = await _BrandSrv.GetAllAsync();
            return View(brandsData);
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
        public async Task<IActionResult> Update([FromBody] BrandsVM brandsVM)
        {
            var result = await _BrandSrv.UpdateAsync(brandsVM);
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
            var result = await _BrandSrv.DeleteAsync(id);
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
