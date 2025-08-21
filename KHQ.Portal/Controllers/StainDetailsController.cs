using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class StainDetailsController : Controller
    {
        private readonly IStainDetailsSrv _stainDetailsSrv;

        public StainDetailsController(IStainDetailsSrv stainDetailsSrv)
        {
            _stainDetailsSrv = stainDetailsSrv;
        }

        public async Task<IActionResult> Index()
        {
            var stainDetails = await _stainDetailsSrv.GetAllAsync();
            return View(stainDetails);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var stainDetails = await _stainDetailsSrv.GetByIdAsync(id);
            return View(stainDetails);
        }

        public async Task<IActionResult> GetByStainId(Guid stainId)
        {
            var stainDetails = await _stainDetailsSrv.GetByStainId(stainId);
            return View(stainDetails);
        }

        public async Task<IActionResult> Delete(Guid id) 
        {
            try
            {
                var result = await _stainDetailsSrv.DeleteAsync(id);
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

        public async Task<IActionResult> Add(StainDetailsVM stainDetailsVM)
        {
            try
            {
                var result = await _stainDetailsSrv.AddAsync(stainDetailsVM);
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

        public async Task<IActionResult> Update(StainDetailsVM stainDetailsVM)
        {
            try
            {
                var result = await _stainDetailsSrv.UpdateAsync(stainDetailsVM);
                if(result > 0)
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
