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
    public class SlidersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public SlidersController(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<SliderDto>> GetAll()
        {
            var slidersData = await _cacheService.GetOrCreateAsync(async () =>
            {
                var sliderData = await _unitOfWork.Repository<Slider>().Queryable().ToListAsync();
                var result = _mapper.Map<List<SliderDto>>(sliderData);
                foreach (var item in result)
                {
                    item.Images = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.F_Key == item.Id).OrderBy(x => x.Sort).Select(x => x.PathLink).ToListAsync();
                }
                return result;
            }, 5 , "SlidersData");
            return slidersData; 
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<SliderDto> GetById(Guid id)
        {
            var sliderData = await _unitOfWork.Repository<Slider>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<SliderDto>(sliderData);
            return result;
        }
    }
}
