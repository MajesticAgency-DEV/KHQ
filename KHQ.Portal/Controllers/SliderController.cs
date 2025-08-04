using KHQ.Domain;
using KHQ.Domain.ViewModel;
using KHQ.Portal.Service;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderSrv _sliderSrv;
        private readonly IImageService _imageService;

        public SliderController(ISliderSrv sliderSrv, IImageService imageService)
        {
            _sliderSrv = sliderSrv;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<SliderVM> sliderData = await _sliderSrv.GetAllAsync();

            foreach (SliderVM sliderVM in sliderData)
            {
                // Initialize PathLink if it's null
                if (sliderVM.PathLink == null)
                {
                    sliderVM.PathLink = new List<string>();
                }

                var images = await _imageService.GetImagesAsync(sliderVM.Id, ImageType.Sliders);

                foreach (var image in images)
                {
                    sliderVM.PathLink.Add(image.PathLink);
                }
            }

            return View(sliderData);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] SliderVM sliderVM)
        {
            try
            {
                var result = await _sliderSrv.AddAsync(sliderVM);
                if (result > 0)
                {
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
        public async Task<IActionResult> Update([FromForm] SliderVM sliderVM)
        {
            try
            {
                var result = await _sliderSrv.UpdateAsync(sliderVM);
                if (result > 0)
                {
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
                var result = await _sliderSrv.DeleteAsync(id);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}