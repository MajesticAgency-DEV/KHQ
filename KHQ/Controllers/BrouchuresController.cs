using AutoMapper;
using KHQ.Caching;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrouchuresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly ICacheService _cacheService;

        public BrouchuresController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<BrouchuresDto>> GetAll()
        {
            var data = await _cacheService.GetOrCreateAsync(async () =>
            {
                var brouchuresData = await _unitOfWork.Repository<Brouchures>().Queryable().ToListAsync();
                return brouchuresData;
            }, 10, "Brouchures");

            var result = _mapper.Map<IEnumerable<BrouchuresDto>>(data);
            return result;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<BrouchuresDto> GetById(Guid id)
        {
            var brouchuresData = await _unitOfWork.Repository<Brouchures>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<BrouchuresDto>(brouchuresData);
            return result;
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var brouchure = await _unitOfWork.Repository<Brouchures>().GetByIdAsync(id);
            if (brouchure == null)
                return NotFound("Brouchure not found.");

            // Get the physical path of the file
            string sharedFolder = Path.Combine(_env.ContentRootPath, "..", "SharedImages");
            if (!Directory.Exists(sharedFolder))
                Directory.CreateDirectory(sharedFolder);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), sharedFolder, brouchure.FilePath.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return NotFound("File not found on server.");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            var fileName = Path.GetFileName(fullPath);

            // Return file for browser download
            return File(fileBytes, "application/pdf", fileName);
        }
    }
}
