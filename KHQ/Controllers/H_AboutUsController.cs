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
    public class H_AboutUsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public H_AboutUsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<H_AboutUsDto> GetAll()
        {
            var h_AboutUsData = await _unitOfWork.Repository<H_AboutUs>().Queryable().ToListAsync();
            var result = _mapper.Map<H_AboutUsDto>(h_AboutUsData);
            return result;
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
