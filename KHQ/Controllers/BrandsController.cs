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
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<BrandDtoNew> GetAll()
        {
            var brandsData = await _unitOfWork.Repository<Brands>().Queryable().ToListAsync();

            var bh_brandsData = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.SectionType == 2).FirstOrDefaultAsync();
            var bh_result = _mapper.Map<BaseHomeDto>(bh_brandsData);

            var result = _mapper.Map<IEnumerable<BrandsDto>>(brandsData);

            var coverPhoto = await _unitOfWork.Repository<Image>().Queryable()
                        .Where(x => x.ImageType == ImageType.Brands_Cover)
                        .FirstOrDefaultAsync();

            foreach (BrandsDto brands in result)
            {
                // Initialize PathLink if it's null
                if (brands.ImageLink == null)
                {
                    brands.ImageLink = "";
                }
                var images = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == ImageType.Brands && x.F_Key == brands.Id).ToListAsync();
                foreach (var image in images)
                {
                    brands.ImageLink = image.PathLink;
                }
            }
            BrandDtoNew brandDtoNew = new BrandDtoNew();
            brandDtoNew.BrandsDtos = (List<BrandsDto>)result;
            brandDtoNew.Main_Description = bh_result.Description;
            brandDtoNew.Title = bh_result.Title;
            brandDtoNew.CoverPhoto = coverPhoto.PathLink;
            return brandDtoNew;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<BrandsDto> GetById(Guid id)
        {
            var brandsData = await _unitOfWork.Repository<Brands>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<BrandsDto>(brandsData);
            return result;
        }
    }
}
