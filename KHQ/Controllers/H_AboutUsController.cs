using AutoMapper;
using KHQ.Srv.Caching;
using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class H_AboutUsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public H_AboutUsController(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<H_AboutUsDtoNew> GetAll()
        {
            var h_AboutUsDto = await _cacheService.GetOrCreateAsync(async () =>
            {
                var about_Data = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.SectionType == (int)SectionType.AboutUs).FirstOrDefaultAsync();
                var h_AboutUsData = await _unitOfWork.Repository<H_AboutUs>().Queryable().ToListAsync();

                return new { h_aboutus = h_AboutUsData, basehome = about_Data };
            });
            var about_result = _mapper.Map<BaseHomeDto>(h_AboutUsDto.basehome);
            var result = _mapper.Map<List<H_AboutUsDto>>(h_AboutUsDto.h_aboutus);

            foreach (var item in result)
            {
                var images = await _unitOfWork.Repository<Domain.Entities.Image>().Queryable().Where(x => x.ImageType == ImageType.AboutUs_Home).ToListAsync();
                item.ImageLink = images[0].PathLink;
            }
            H_AboutUsDtoNew h_aboutus = new H_AboutUsDtoNew();
            h_aboutus.H_Aboutus = result;
            h_aboutus.Main_Title = about_result.Title;
            h_aboutus.Main_Description = about_result.Description;
            return h_aboutus;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<H_AboutUsDto> GetById(Guid id)
        {
            var h_AboutUsData = await _unitOfWork.Repository<H_AboutUs>().Queryable().Where(x => x.ID == id).FirstOrDefaultAsync();
            var result = _mapper.Map<H_AboutUsDto>(h_AboutUsData);
            return result;
        }
    }
}
