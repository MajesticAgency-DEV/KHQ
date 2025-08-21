using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class StainsController : Controller
    {
        private readonly IStainsService _stainsService;

        public StainsController(IStainsService stainsService)
        {
            _stainsService = stainsService;
        }
        public async Task<IActionResult> Index()
        {
            var stains = await _stainsService.GetAllAsync();
            return View(stains);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var stains = await _stainsService.GetByIdAsync(id);
            return View(stains);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _stainsService.DeleteAsync(id);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> Add(StainsVM stainsVM)
        {
            try
            {

                var result = await _stainsService.AddAsync(stainsVM);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> Update(StainsVM stainsVM)
        {
            try
            {

                var result = await _stainsService.UpdateAsync(stainsVM);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
