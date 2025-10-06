using AutoMapper;
using KHQ.Caching;
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
        private readonly ICacheService _cacheService;

        public ContactUsController(IUnitOfWork unitOfWork, IMapper mapper,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ContactUsDto> GetAll()
        {
            var data = await _cacheService.GetOrCreateAsync(async () =>
            {
                var contactUsData = await _unitOfWork.Repository<ContactUs>().Queryable().FirstOrDefaultAsync();
                var coverPhoto = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == ImageType.ContactUs_Cover).FirstOrDefaultAsync();
                return new {contactUs = contactUsData,coverPhoto = coverPhoto};
            }, 20 , "ContactUS");
            
            var result = _mapper.Map<ContactUsDto>(data.contactUs);

            if (result != null)
                result.CoverPhoto = data.coverPhoto == null ? "" : data.coverPhoto.PathLink;

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
