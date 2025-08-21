using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUsSrv _ContactUsSrv;

        public ContactUsController(IContactUsSrv contactUsSrv)
        {
            _ContactUsSrv = contactUsSrv;
        }
        public async Task<IActionResult> Index()
        {
            var contactUsData = await _ContactUsSrv.GetAllAsync();
            return View(contactUsData);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var contactUsData = await _ContactUsSrv.GetByIdAsync(id);
            return View(contactUsData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContactUsVM contactUsVM)
        {
            var result = await _ContactUsSrv.AddAsync(contactUsVM);
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
        public async Task<IActionResult> Update([FromBody] ContactUsVM contactUsVM)
        {
            var result = await _ContactUsSrv.UpdateAsync(contactUsVM);
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
            var result = await _ContactUsSrv.DeleteAsync(id);
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
