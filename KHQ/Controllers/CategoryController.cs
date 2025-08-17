using AutoMapper;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<CategoryDto> GetAll()
        {
            var categoryData = await _unitOfWork.Repository<Category>().GetAllAsync();
            var result = _mapper.Map<CategoryDto>(categoryData);
            return result;
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
