using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Portal.Service;
using KHQ.Srv.Caching;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class StainsController : Controller
    {
        private readonly IStainsService _stainsService;
        private readonly IStainDetailsSrv _stainDetailsSrv;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;


        public StainsController(IStainsService stainsService, IStainDetailsSrv stainDetailsSrv, IImageService imageService, ICacheService cacheService)
        {
            _stainsService = stainsService;
            _stainDetailsSrv = stainDetailsSrv;
            _imageService = imageService;
            _cacheService = cacheService;
        }
        public async Task<IActionResult> Index()
        {
            var stains = await _stainsService.GetAllAsync();

            foreach (StainsVM stainVM in stains)
            {
                // Initialize PathLink if it's null
                if (stainVM.ImageLink == null)
                {
                    stainVM.ImageLink = "";
                }
                var images = await _imageService.GetImagesAsync(stainVM.Id, ImageType.Stains);

                foreach (var image in images)
                {
                    stainVM.ImageLink = image.PathLink;
                }
            }

            return View(stains);
        }

        public async Task<IActionResult> CoverSection()
        {
            var images = await _imageService.GetImagesByImageTypeAsync(ImageType.Stains_Cover);
            return View(images);
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

                // Initialize PathLink if it's null
                if (item.ImageLink == null)
                {
                    item.ImageLink = "";
                }
                var images = await _imageService.GetImagesAsync(item.Id, ImageType.StainsDetails);

                foreach (var image in images)
                {
                    item.ImageLink = image.PathLink;
                }
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
                var staindetailsToBeDeleted = await _stainDetailsSrv.GetByStainId(id);
                var x = staindetailsToBeDeleted == null ? 0 : await _stainDetailsSrv.DeleteAsync(staindetailsToBeDeleted.Id);
                if (result > 0)
                {
                    _cacheService.ClearAll();
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
                    _cacheService.ClearAll();
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
                    _cacheService.ClearAll();
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
