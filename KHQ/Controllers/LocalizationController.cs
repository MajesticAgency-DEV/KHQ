using Microsoft.AspNetCore.Mvc;

namespace KHQ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationController : ControllerBase
    {
        [HttpPost("set-culture")]
        public IActionResult SetCulture([FromQuery] string culture)
        {
            return Ok(new { culture = string.IsNullOrWhiteSpace(culture) ? "en" : culture });
        }
    }
}


