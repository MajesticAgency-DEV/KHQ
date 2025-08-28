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
    public class SlidersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SlidersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<SliderDto> GetAll()
        {
            var sliderData = await _unitOfWork.Repository<Slider>().Queryable().ToListAsync();
            var result = _mapper.Map<SliderDto>(sliderData);
            return result;
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
