using AutoMapper;
using KHQ.Srv.Caching;
using KHQ.Domain;
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
    public class AboutUsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public AboutUsController(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<AboutUsDto>> GetAll()
        {
            var aboutUs = await _cacheService.GetOrCreateAsync(async () =>
            {
                var aboutUsData = await _unitOfWork.Repository<AboutUs>().Queryable().ToListAsync();
                var points = await _unitOfWork.Repository<H_AboutUs>().Queryable().ToListAsync();
                var coverPhoto = await _unitOfWork.Repository<Image>().Queryable()
                                        .Where(x => x.ImageType == ImageType.AboutUs_Cover)
                                        .FirstOrDefaultAsync();
                var aboutUsImages = await _unitOfWork.Repository<Image>().Queryable()
                                        .Where(x => x.ImageType == ImageType.AboutUs_Page)
                                        .OrderBy(x => x.Id)
                                        .ToListAsync();

                var result = _mapper.Map<List<AboutUsDto>>(aboutUsData);

                // Assign points and images sequentially
                for (int i = 0; i < result.Count; i++)
                {
                    var item = result[i];

                    // Assign all H_AboutUs points
                    item.H_AboutUsDto = _mapper.Map<IEnumerable<H_AboutUsDto>>(points);

                    // Assign same cover photo for all
                    item.CoverPhoto = coverPhoto?.PathLink ?? string.Empty;

                    // Assign image sequentially
                    if (i < aboutUsImages.Count)
                        item.AboutUsImage = aboutUsImages[i].PathLink;
                    else
                        item.AboutUsImage = string.Empty;
                }

                return result;
            }, 3, "AboutUs_All");

            return aboutUs;
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<AboutUsDto> GetById(Guid id)
        {
            var aboutUsData = await _unitOfWork.Repository<AboutUs>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<AboutUsDto>(aboutUsData);
            return result;
        }

    }
}
