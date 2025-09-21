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
    public class WhyChooseUsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WhyChooseUsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<WhyChooseUsDto>> GetAll()
        {
            var whyChooseUsData = await _unitOfWork.Repository<WhyChooseUs>().Queryable().ToListAsync();
            var result = _mapper.Map<IEnumerable<WhyChooseUsDto>>(whyChooseUsData);
            return result;
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
