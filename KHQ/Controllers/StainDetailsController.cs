using AutoMapper;
using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StainDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StainDetailsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IList<StainDetailsDto>> GetAll()
        {
            var stainDetailsData = await _unitOfWork.Repository<StainDetails>().Queryable().ToListAsync();
            var result = _mapper.Map<IList<StainDetailsDto>>(stainDetailsData);
            return result;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<StainDetailsDto> GetById(Guid id)
        {
            var stainDetailsData = await _unitOfWork.Repository<StainDetails>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<StainDetailsDto>(stainDetailsData);
            return result;
        }

        [HttpGet]
        [Route("GetByStainsId/{id}")]
        public async Task<StainDetailsDto> GetByStainsId(Guid id)
        {
            var stainDetailsData = await _unitOfWork.Repository<StainDetails>().Queryable().Where(x => x.StainsId == id).FirstOrDefaultAsync();
            var result = _mapper.Map<StainDetailsDto>(stainDetailsData);

            var image = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == ImageType.StainsDetails && x.F_Key == result.Id).FirstOrDefaultAsync();
            var imageCover = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == ImageType.StainsDetails_Cover).FirstOrDefaultAsync();

            result.ImageLink = image != null ? image.PathLink : "";
            result.CoverLink = imageCover != null ? imageCover.PathLink : "";
            return result;
        }
    }
}
