using AutoMapper;
using KHQ.Caching;
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
    public class FAQController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public FAQController(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<FAQDtoNew> GetAll()
        {
            var faqs = await _cacheService.GetOrCreateAsync(async () => {

                var result = await _unitOfWork.Repository<FAQ>().Queryable().ToListAsync();
                //var brouchures = await _unitOfWork.Repository<Brouchures>().Queryable().FirstOrDefaultAsync();
                var coverPhoto = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == Domain.ImageType.FAQ_Cover).FirstOrDefaultAsync();
                return new {faq = result , coverphoto = coverPhoto};
            });

            var faqData = _mapper.Map<List<FAQDto>>(faqs.faq);
            //var brouchuresData = _mapper.Map<BrouchuresDto>(faqs.brouchures);

            FAQDtoNew fAQ = new FAQDtoNew();
            fAQ.FAQs = faqData;
            //fAQ.Brouchures = brouchuresData;
            fAQ.CoverPhoto = faqs.coverphoto.PathLink;

            return fAQ;

        }

    }
}
