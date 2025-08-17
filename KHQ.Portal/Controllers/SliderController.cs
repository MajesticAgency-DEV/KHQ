using KHQ.Domain;
using KHQ.Domain.ViewModel;
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
            var sliderData = await _sliderSrv.GetAllAsync();
            return View(sliderData);
        }

        public async Task<IActionResult> GetById(Guid id)   
        {
            var sliderData = await _sliderSrv.GetByIdAsync(id); 
            return View(sliderData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SliderVM sliderVM, List<IFormFile> files)
        {
            try
            {
                var result = await _sliderSrv.AddAsync(sliderVM);
                if (result > 0)
                {
                    var images = await _imageService.SaveImagesAsync(files, sliderVM.Id, ImageType.Sliders);
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


        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] SliderVM sliderVM)
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

        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _sliderSrv.DeleteAsync(id);
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

        //private async Task<string> SaveImageAsync(IFormFile imageUpload)
        //{
        //    if (imageUpload == null || imageUpload.Length == 0)
        //    {
        //        throw new ArgumentException("Invalid image upload");
        //    }

        //    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        //    if (!Directory.Exists(uploadsFolder))
        //    {
        //        Directory.CreateDirectory(uploadsFolder);
        //    }

        //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageUpload.FileName);
        //    var filePath = Path.Combine(uploadsFolder, fileName);

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await imageUpload.CopyToAsync(fileStream);
        //    }

        //    return fileName;
        //}
    }
}
