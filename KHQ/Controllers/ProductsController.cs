using AutoMapper;
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
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var productData = await _unitOfWork.Repository<Product>().Queryable().Include(x => x.SubProducts).ToListAsync();
            var result = _mapper.Map<IEnumerable<ProductDto>>(productData);
            return result;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ProductDto> GetById(Guid id)
        {
            var productData = await _unitOfWork.Repository<Product>().Queryable().Include(x => x.SubProducts).Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<ProductDto>(productData);
            return result;
        }
    }
}
