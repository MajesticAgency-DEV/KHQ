using AutoMapper;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseHomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BaseHomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<BaseHomeDto>> GetAll()
        {
            var baseHomeData = await _unitOfWork.Repository<BaseHome>().Queryable().ToListAsync();
            var result = _mapper.Map<IEnumerable<BaseHomeDto>>(baseHomeData);
            return result;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<BaseHomeDto> GetById(Guid id)
        {
            var baseHomeData = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<BaseHomeDto>(baseHomeData);
            return result;
        }

        [HttpGet]
        [Route("GetByType/{type}")]
        public async Task<BaseHomeDto> GetByType(int type)
        {
            var baseHomeData = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.SectionType == type).FirstOrDefaultAsync();
            var result = _mapper.Map<BaseHomeDto>(baseHomeData);
            return result;
        }
    }
}
