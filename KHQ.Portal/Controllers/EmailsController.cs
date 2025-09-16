using AutoMapper;
using KHQ.Portal.Service;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class EmailsController : Controller
    {
        private readonly IEmailsSrv _emailsSrv;
        private readonly IMapper _mapper;

        public EmailsController(IEmailsSrv emailsSrv, IMapper mapper)
        {
            _emailsSrv = emailsSrv;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var emailsData = await _emailsSrv.GetAllAsync();

            return View(emailsData);
        }
        public async Task<IActionResult> GetById(Guid id)
        {
            var emailData = await _emailsSrv.GetByIdAsync(id);
            return View(emailData);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _emailsSrv.DeleteAsync(id);
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
