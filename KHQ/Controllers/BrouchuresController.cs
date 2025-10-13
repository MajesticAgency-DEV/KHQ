using AutoMapper;
using KHQ.Srv.Caching;
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
        public async Task<BrouchuresDtoNew> GetAll()
        {
            var brouchuresData = await _unitOfWork.Repository<Brouchures>().Queryable().ToListAsync();
            var data = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.SectionType == 8).FirstOrDefaultAsync();
            var b_data = _mapper.Map<BaseHomeDto>(data);

            BrouchuresDtoNew brouchuresDtoNew = new BrouchuresDtoNew();
            brouchuresDtoNew.BrouchuresDto = brouchuresData;
            brouchuresDtoNew.Title = b_data.Title;
            brouchuresDtoNew.Description = b_data.Description;

            return brouchuresDtoNew;
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
