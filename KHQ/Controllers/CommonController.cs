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
    public class CommonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommonController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetImageById/{id}")]
        public async Task<IEnumerable<ImageDto>> GetImageById(Guid id)
        {
            var imageData = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<IEnumerable<ImageDto>>(imageData);
            return result;
        }

        [HttpGet]
        [Route("GetImageByFKey/{id}")]
        public async Task<IEnumerable<ImageDto>> GetImageByFKey(Guid id)
        {
            var imageData = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.F_Key == id).FirstOrDefaultAsync();
            var result = _mapper.Map<IEnumerable<ImageDto>>(imageData);
            return result;
        }

        [HttpGet]
        [Route("GetImageByType/{imageType}")]
        public async Task<IEnumerable<ImageDto>> GetImageByType(ImageType imageType)
        {
            var imageData = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == imageType).FirstOrDefaultAsync();
            var result = _mapper.Map<IEnumerable<ImageDto>>(imageData);
            return result;
        }
    }
}
