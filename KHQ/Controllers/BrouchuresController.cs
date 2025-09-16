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
    public class BrouchuresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrouchuresController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<BrouchuresDto>> GetAll()
        {
            var brouchuresData = await _unitOfWork.Repository<Brouchures>().Queryable().ToListAsync();
            var result = _mapper.Map<IEnumerable<BrouchuresDto>>(brouchuresData);
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
    }
}
