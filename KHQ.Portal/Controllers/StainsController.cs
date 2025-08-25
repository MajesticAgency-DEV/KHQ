using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class StainsController : Controller
    {
        private readonly IStainsService _stainsService;
        private readonly IStainDetailsSrv _stainDetailsSrv;


        public StainsController(IStainsService stainsService, IStainDetailsSrv stainDetailsSrv)
        {
            _stainsService = stainsService;
            _stainDetailsSrv = stainDetailsSrv;
        }
        public async Task<IActionResult> Index()
        {
            var stains = await _stainsService.GetAllAsync();
            return View(stains);
        }
        [HttpGet]
        [Route("GetAllStatin")]
        public async Task<IEnumerable<StainsVM>> GetAllStains()
        {
            var stains = await _stainsService.GetAllAsync();
            return stains;
        }
        public async Task<IActionResult> StainDetails()
        {
            var stainDetails = await _stainDetailsSrv.GetAllAsync();
            foreach (var item in stainDetails)
            {
                item.StainsVM = await _stainsService.GetByIdAsync(item.StainsId);
            }
            return View(stainDetails);
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
