using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;
using Microsoft.EntityFrameworkCore;

namespace KHQ.Srv.Services
{
    public class StainDetailsSrv : IStainDetailsSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StainDetailsSrv(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(StainDetailsVM entity)
        {
            var stainDetailsToBeAdded = _mapper.Map<StainDetails>(entity);
            await _unitOfWork.Repository<StainDetails>().AddAsync(stainDetailsToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var stainDetailsToBeDeleted = await _unitOfWork.Repository<StainDetails>().GetByIdAsync(id);
            if (stainDetailsToBeDeleted != null)
            {
                _unitOfWork.Repository<StainDetails>().Delete(stainDetailsToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<StainDetailsVM>> GetAllAsync()
        {
            var stainDetails = await _unitOfWork.Repository<StainDetails>().GetAllAsync();
            return _mapper.Map<IEnumerable<StainDetailsVM>>(stainDetails);
        }

        public async Task<StainDetailsVM?> GetByIdAsync(Guid id)
        {
            var stainDetails = await _unitOfWork.Repository<StainDetails>().GetByIdAsync(id);
            return _mapper.Map<StainDetailsVM>(stainDetails);
        }

        public async Task<StainDetailsVM?> GetByStainId(Guid id)
        {
            var stainDetails = await _unitOfWork.Repository<StainDetails>().Queryable().Where(x => x.StainsId == id).FirstOrDefaultAsync();
            return _mapper.Map<StainDetailsVM>(stainDetails);
        }

        public async Task<int> UpdateAsync(StainDetailsVM entity)
        {
            var stainDetailsToBeUpdated = _mapper.Map<StainDetails>(entity);
            _unitOfWork.Repository<StainDetails>().Update(stainDetailsToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
