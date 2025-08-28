using AutoMapper;
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

        public AboutUsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<AboutUsDto>> GetAll()
        {
            var aboutUsData = await _unitOfWork.Repository<AboutUs>().Queryable().ToListAsync();
            var result = _mapper.Map<IEnumerable<AboutUsDto>>(aboutUsData);
            return result;
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
