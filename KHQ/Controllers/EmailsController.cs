using AutoMapper;
using KHQ.Domain.DTOs;
using KHQ.Repo.UOW;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailsSrv _emailsSrv;

        public EmailsController(IEmailsSrv emailsSrv)
        {
            _emailsSrv = emailsSrv;
        }

        [HttpPost]
        [Route("AddEmail")]
        public async Task<IActionResult> AddEmail(EmailsDto emailsDto)
        {
            var result = await _emailsSrv.AddAsync(emailsDto);
            return Ok();
        }
    }
}
