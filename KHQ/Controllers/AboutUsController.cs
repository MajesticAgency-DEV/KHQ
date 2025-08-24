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

        public async Task<AboutUsDto> GetAll()
        {
            var aboutUsData = await _unitOfWork.Repository<AboutUs>().Queryable().ToListAsync();
            var result = _mapper.Map<AboutUsDto>(aboutUsData);
            return result;
        }

        public async Task<AboutUsDto> GetById(Guid id)
        {
            var aboutUsData = await _unitOfWork.Repository<AboutUs>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<AboutUsDto>(aboutUsData);
            return result;
        }

        public async Task<AboutUsDto> GetAboutUsByImage(ImageType imageType)
        {
            var aboutUsCover = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == imageType).FirstOrDefaultAsync();
            var result = _mapper.Map<AboutUsDto>(aboutUsCover);
            return result;
        }
    }
}
