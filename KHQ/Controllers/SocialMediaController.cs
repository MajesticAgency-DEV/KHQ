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
    public class SocialMediaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SocialMediaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<SocialMediaDto>> GetAll()
        {
            var socialMediaData = await _unitOfWork.Repository<SocialMedia>().Queryable().ToListAsync();
            var result = _mapper.Map<IEnumerable<SocialMediaDto>>(socialMediaData);
            return result;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<SocialMediaDto> GetById(Guid id)
        {
            var socialMediaData = await _unitOfWork.Repository<SocialMedia>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<SocialMediaDto>(socialMediaData);
            return result;
        }
    }
}
