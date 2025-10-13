using AutoMapper;
using KHQ.Srv.Caching;
using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhyChooseUsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public WhyChooseUsController(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<WhyChooseUsDtoNew> GetAll()
        {
            var whyChooseUsDto = await _cacheService.GetOrCreateAsync(async () =>
            {
                var whyChooseUs_Data = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.SectionType == (int)SectionType.WhyChooseUs).FirstOrDefaultAsync();
                var whyChooseUsData = await _unitOfWork.Repository<WhyChooseUs>().Queryable().ToListAsync();

                return new { whyChooseUs = whyChooseUsData, basehome = whyChooseUs_Data };
            });
            var whyChooseUs_result = _mapper.Map<BaseHomeDto>(whyChooseUsDto.basehome);
            var result = _mapper.Map<List<WhyChooseUsDto>>(whyChooseUsDto.whyChooseUs);

            WhyChooseUsDtoNew whyChooseUs = new WhyChooseUsDtoNew();
            whyChooseUs.WhyChooseUs = result;
            whyChooseUs.Main_Title = whyChooseUs_result.Title;
            whyChooseUs.Main_Description = whyChooseUs_result.Description;
            return whyChooseUs;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<WhyChooseUsDto> GetById(Guid id)
        {
            var whyChooseUsData = await _unitOfWork.Repository<WhyChooseUs>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<WhyChooseUsDto>(whyChooseUsData);
            return result;
        }
    }
}
