using AutoMapper;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KHQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseHomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BaseHomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseHomeDto> GetAll()
        {
            var baseHomeData = await _unitOfWork.Repository<BaseHome>().Queryable().ToListAsync();
            var result = _mapper.Map<BaseHomeDto>(baseHomeData);
            return result;
        }

        public async Task<BaseHomeDto> GetById(Guid id)
        {
            var baseHomeData = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<BaseHomeDto>(baseHomeData);
            return result;
        }
    }
}
