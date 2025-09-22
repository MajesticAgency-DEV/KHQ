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
    public class ContactUsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactUsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ContactUsDto> GetAll()
        {
            var contactUsData = await _unitOfWork.Repository<ContactUs>().Queryable().FirstOrDefaultAsync();
            var coverPhoto = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == ImageType.ContactUs_Cover).FirstOrDefaultAsync();

            var result = _mapper.Map<ContactUsDto>(contactUsData);

            if (result != null)
                result.CoverPhoto = coverPhoto == null ? "" : coverPhoto.PathLink;

            return result;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ContactUs> GetById(Guid id)
        {
            var result = await _unitOfWork.Repository<ContactUs>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }
    }
}
