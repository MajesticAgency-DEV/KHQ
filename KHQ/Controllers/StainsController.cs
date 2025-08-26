using AutoMapper;
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
    public class StainsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StainsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<StainsDto>> GetAll()
        {
            var stainDetailsData = await _unitOfWork.Repository<Stains>().Queryable().ToListAsync();
            var result = _mapper.Map<IEnumerable<StainsDto>>(stainDetailsData);
            return result;
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
