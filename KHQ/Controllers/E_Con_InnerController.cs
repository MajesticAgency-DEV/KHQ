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
    public class E_Con_InnerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public E_Con_InnerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<E_Con_InnerDto> GetAll()
        {
            var e_Con_InnerData = await _unitOfWork.Repository<E_Con_Inner>().Queryable().ToListAsync();
            var result = _mapper.Map<E_Con_InnerDto>(e_Con_InnerData);
            return result;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<E_Con_InnerDto> GetById(Guid id)
        {
            var e_Con_InnerData = await _unitOfWork.Repository<E_Con_Inner>().Queryable().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<E_Con_InnerDto>(e_Con_InnerData);
            return result;
        }
    }
}
