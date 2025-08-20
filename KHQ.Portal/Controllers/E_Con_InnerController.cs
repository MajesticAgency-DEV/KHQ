using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class E_Con_InnerController : Controller
    {
        private readonly IEConInnerService _EConInnerService;

        public E_Con_InnerController(IEConInnerService eConInnerService)
        {
            _EConInnerService = eConInnerService;
        }
        public async Task<IActionResult> Index()
        {
            var e_Con_InnerData = await _EConInnerService.GetAllAsync();
            return View(e_Con_InnerData);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var e_Con_InnerData = await _EConInnerService.GetByIdAsync(id);
            return View(e_Con_InnerData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(E_Con_InnerVM e_Con_InnerVM)
        {
            var result = await _EConInnerService.AddAsync(e_Con_InnerVM);
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
        public async Task<IActionResult> Update(E_Con_InnerVM e_Con_InnerVM)
        {
            var result = await _EConInnerService.UpdateAsync(e_Con_InnerVM);
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
            var result = await _EConInnerService.DeleteAsync(id);
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
