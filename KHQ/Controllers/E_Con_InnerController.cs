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
    public class E_Con_InnerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public E_Con_InnerController(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<Statistics> GetAll()
        {

            var statestics = await _cacheService.GetOrCreateAsync(async () =>
            {
                var sh_Data = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.SectionType == 7).FirstOrDefaultAsync();
                
                var e_Con_InnerData = await _unitOfWork.Repository<E_Con_Inner>().Queryable().ToListAsync();
                
                return new { home_Data = sh_Data, statesticsData = e_Con_InnerData } ;
            });
            var bh_result = _mapper.Map<BaseHomeDto>(statestics.home_Data);
            var result = _mapper.Map<List<E_Con_InnerDto>>(statestics.statesticsData);
            Statistics statisticsResult = new Statistics();
            statisticsResult.statistics = result;
            statisticsResult.Main_Title = bh_result.Title;
            statisticsResult.Main_Description = bh_result.Description;
            return statisticsResult;
            
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<E_Con_InnerDto> GetById(Guid id)
        {
            var e_Con_InnerData = await _unitOfWork.Repository<E_Con_Inner>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<E_Con_InnerDto>(e_Con_InnerData);
            return result;
        }
    }
}
