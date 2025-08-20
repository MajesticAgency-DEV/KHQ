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
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BrandsDto> GetAll()
        {
            var brandsData = await _unitOfWork.Repository<Brands>().Queryable().ToListAsync();
            var result = _mapper.Map<BrandsDto>(brandsData);
            return result;
        }

        public async Task<BrandsDto> GetById(Guid id)
        {
            var brandsData = await _unitOfWork.Repository<Brands>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<BrandsDto>(brandsData);
            return result;
        }
    }
}
