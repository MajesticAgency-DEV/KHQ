using KHQ.Domain;
using KHQ.Domain.ViewModel;
using KHQ.Portal.Service;
using KHQ.Srv.Caching;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class StainDetailsController : Controller
    {
        private readonly IStainDetailsSrv _stainDetailsSrv;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;

        public StainDetailsController(IStainDetailsSrv stainDetailsSrv, IImageService imageService, ICacheService cacheService)
        {
            _stainDetailsSrv = stainDetailsSrv;
            _imageService = imageService;
            _cacheService = cacheService;
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
                    _cacheService.ClearAll();
                    return Json(new { success = true, message = "Stain details deleted successfully" });
                }
                return Json(new { success = false, message = "Failed to delete stain details" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting stain details" });
            }
        }

        public async Task<IActionResult> Add(StainDetailsVM stainDetailsVM)
        {
            try
            {
                var result = await _stainDetailsSrv.AddAsync(stainDetailsVM);
                if (result > 0)
                {
                    _cacheService.ClearAll();
                    return Json(new { success = true, message = "Stain details added successfully" });
                }
                return Json(new { success = false, message = "Failed to add stain details" });
            }
            catch (Exception ex)
            {
                // Log the exception if you have logging
                return Json(new { success = false, message = "An error occurred while adding stain details" });
            }
        }

        public async Task<IActionResult> Update(StainDetailsVM stainDetailsVM)
        {
            try
            {
                var result = await _stainDetailsSrv.UpdateAsync(stainDetailsVM);
                if (result > 0)
                {
                    _cacheService.ClearAll();
                    return Json(new { success = true, message = "Stain details updated successfully" });
                }
                return Json(new { success = false, message = "Failed to update stain details" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating stain details" });
            }
        }

        public async Task<IActionResult> CoverSection()
        {
            var images = await _imageService.GetImagesByImageTypeAsync(ImageType.StainsDetails_Cover);
            return View(images);
        }
    }
}
