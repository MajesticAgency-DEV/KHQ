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
        public async Task<AboutUsDto> GetAll()
        {
            var aboutUsData = await _unitOfWork.Repository<AboutUs>().Queryable().FirstOrDefaultAsync();
            var points = await _unitOfWork.Repository<H_AboutUs>().Queryable().ToListAsync();
            var coverPhoto = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == ImageType.AboutUs_Cover).FirstOrDefaultAsync();
            var aboutUsImage = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.F_Key == aboutUsData.Id).FirstOrDefaultAsync();
            
            var result = _mapper.Map<AboutUsDto>(aboutUsData);
            result.H_AboutUsDto = _mapper.Map<IEnumerable<H_AboutUsDto>>(points);
            result.CoverPhoto = coverPhoto == null ? "" : coverPhoto.PathLink;
            result.AboutUsImage = aboutUsImage == null ? "" : aboutUsImage.PathLink;
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
