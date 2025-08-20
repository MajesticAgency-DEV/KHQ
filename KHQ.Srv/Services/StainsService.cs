using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Srv.Services
{
    public class StainsService : IStainsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StainsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(StainsVM entity)
        {
            var stainsToBeAdded = _mapper.Map<Stains>(entity);
            await _unitOfWork.Repository<Stains>().AddAsync(stainsToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var stainsToBeDeleted = await _unitOfWork.Repository<Stains>().GetByIdAsync(id);
            if (stainsToBeDeleted != null)
            {
                _unitOfWork.Repository<Stains>().Delete(stainsToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<StainsVM>> GetAllAsync()
        {
            var stains = await _unitOfWork.Repository<Stains>().GetAllAsync();
            return _mapper.Map<IEnumerable<StainsVM>>(stains);
        }

        public async Task<StainsVM?> GetByIdAsync(Guid id)
        {
            var stains = await _unitOfWork.Repository<Stains>().GetByIdAsync(id);
            return _mapper.Map<StainsVM>(stains);
        }

        public async Task<int> UpdateAsync(StainsVM entity)
        {
            var stainsToBeUpdated = _mapper.Map<Stains>(entity);
            _unitOfWork.Repository<Stains>().Update(stainsToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
