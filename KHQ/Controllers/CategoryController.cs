using AutoMapper;
using KHQ.Caching;
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
	public class CategoryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ICacheService _cacheService;

		public CategoryController(IUnitOfWork unitOfWork, IMapper mapper,ICacheService cacheService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_cacheService = cacheService;
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<CategoryDtoNew> GetAll()
		{
			var data = await _cacheService.GetOrCreateAsync(async() =>
			{
                var categoryData = await _unitOfWork.Repository<Category>().Queryable().ToListAsync();
                var baseHome = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.SectionType == (int)SectionType.Category).FirstOrDefaultAsync();
                var coverPhoto = await _unitOfWork.Repository<Image>().Queryable()
                .Where(x => x.ImageType == ImageType.Categories_Cover)
                .FirstOrDefaultAsync();

				return new { catData = categoryData, homeData = baseHome, coverPhoto = coverPhoto };

            }, 10 , "Categories");
			var baseHomeDto = _mapper.Map<BaseHomeDto>(data.homeData);

			var result = _mapper.Map<IEnumerable<CategoryDto>>(data.catData);


            foreach (CategoryDto cat in result)
            {
                if (cat.ImageLink == null)
                {
                    cat.ImageLink = "";
                }

                var images = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.ImageType == ImageType.Categories && x.F_Key == cat.Id).ToListAsync();

                foreach (var image in images)
                {
                    cat.ImageLink = image.PathLink;
                }
            }
            var dto = new CategoryDtoNew
			{
				CategoriesDtos = result.ToList(),
				Title = (baseHomeDto?.Title) ?? "Categories",
				Main_Description = (baseHomeDto?.Description) ?? "Categories",
				CoverPhoto = data.coverPhoto.PathLink
            };
			return dto;
		}

		[HttpGet]
		[Route("GetById/{id}")]
		public async Task<CategoryDto> GetById(Guid id)
		{
			var categoryData = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
			var result = _mapper.Map<CategoryDto>(categoryData);
			return result;
		}
	}
}
