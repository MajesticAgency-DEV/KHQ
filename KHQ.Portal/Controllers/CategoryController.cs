using KHQ.Domain;
using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategorySrv _CategorySrv;
        private readonly IImageService _imageService;

        public CategoryController(ICategorySrv categorySrv, IImageService imageService)
        {
            _CategorySrv = categorySrv;
            _imageService = imageService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryVM> categoryData = await _CategorySrv.GetAllAsync();

            foreach (CategoryVM categoryVM in categoryData)
            {
                // Initialize PathLink if it's null
                if (categoryVM.ImageLink == null)
                {
                    categoryVM.ImageLink = "";
                }

                var images = await _imageService.GetImagesAsync(categoryVM.Id, ImageType.Categories);

                foreach (var image in images)
                {
                    categoryVM.ImageLink = image.PathLink;
                }
            }
            return View(categoryData);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var categoryData = await _CategorySrv.GetByIdAsync(id);
            return View(categoryData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryVM categoryVM)
        {
            var result = await _CategorySrv.AddAsync(categoryVM);
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
        public async Task<IActionResult> Update(CategoryVM categoryVM)
        {
            var result = await _CategorySrv.UpdateAsync(categoryVM);
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
            var result = await _CategorySrv.DeleteAsync(id);
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
