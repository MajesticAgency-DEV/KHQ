using AutoMapper;
using KHQ.Caching;
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
    public class StainsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StainsController(IUnitOfWork unitOfWork, IMapper mapper,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<StainsDtoNew> GetAll()
        {
            var stainsDto = await _cacheService.GetOrCreateAsync(async () =>
            {
                var sh_Data = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.SectionType == (int)SectionType.Stains).FirstOrDefaultAsync();               
                var stainDetailsData = await _unitOfWork.Repository<Stains>().Queryable().ToListAsync();
                
                return new { stains = stainDetailsData, basehome = sh_Data };
            });
            var bh_result = _mapper.Map<BaseHomeDto>(stainsDto.basehome);
            var result = _mapper.Map<List<StainsDto>>(stainsDto.stains);

            var coverPhoto = await _unitOfWork.Repository<Image>().Queryable()
                            .Where(x => x.ImageType == ImageType.Stains_Cover)
                            .FirstOrDefaultAsync();
            foreach (var item in result)
            {
                var images = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == ImageType.Stains && x.F_Key == item.Id).ToListAsync();
                foreach (var image in images)
                {
                    item.ImageLink = image.PathLink;
                }
            }
            StainsDtoNew stain = new StainsDtoNew();
            stain.Stains = result;
            stain.Main_Title = bh_result.Title;
            stain.Main_Description = bh_result.Description;
            stain.CoverPhoto = coverPhoto.PathLink;
            return stain;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<StainsDto> GetById(Guid id)
        {
            var stainDetailsData = await _unitOfWork.Repository<Stains>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<StainsDto>(stainDetailsData);
            return result;
        }
    }
}
