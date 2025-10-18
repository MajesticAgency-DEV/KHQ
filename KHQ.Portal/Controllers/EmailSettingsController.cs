using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class EmailSettingsController : Controller
    {
        private readonly IEmailSettingsSRV _emailSettingsSRV;

        public EmailSettingsController(IEmailSettingsSRV emailSettingsSRV)
        {
            _emailSettingsSRV = emailSettingsSRV;
        }

        public async Task<IActionResult> Index()
        {
            var emailSettingsData = await _emailSettingsSRV.GetAllAsync();
            return View(emailSettingsData);
        }

        public async Task<IActionResult> Add(EmailSettingsVM emailSettings)
        {
            var result = await _emailSettingsSRV.AddAsync(emailSettings);
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Update(EmailSettingsVM emailSettings)
        {
            var result = await _emailSettingsSRV.UpdateAsync(emailSettings);
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _emailSettingsSRV.DeleteAsync(id);
            if (result > 0)
            {
                return Ok();
            }
            else { return BadRequest(); }
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _emailSettingsSRV.GetByIdAsync(id);
            return View(data);
        }
    }
}
