using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class BaseHomeController : Controller
    {
        private readonly IBaseHomeService _BaseHomeService;

        public BaseHomeController(IBaseHomeService baseHomeService)
        {
            _BaseHomeService = baseHomeService;
        }
        public async Task<IActionResult> Index()
        {
            var baseHomeData = await _BaseHomeService.GetAllAsync();
            return View(baseHomeData);
        }

        [HttpGet]
        public async Task<IActionResult> GetSectionData(int sectionType)
        {
            try
            {
                var data = await _BaseHomeService.GetSectionDataAsync(sectionType);
                if (data != null)
                {
                    return Json(new { exists = true, data });
                }
                return Json(new { exists = false });
            }
            catch (Exception ex)
            {
                return Json(new { exists = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveSection(BaseHomeVM request)
        {
            try
            {
                var result = await _BaseHomeService.SaveSectionAsync(request);
                if (result > 0)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Failed to save section" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
