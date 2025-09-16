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
    public class BrouchuresSrv : IBrouchuresSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrouchuresSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(BrouchuresVM entity)
        {
            var brouchuresToBeAdded = _mapper.Map<Brouchures>(entity);
            await _unitOfWork.Repository<Brouchures>().AddAsync(brouchuresToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var brouchuresToBeDeleted = await _unitOfWork.Repository<Brouchures>().GetByIdAsync(id);
            if (brouchuresToBeDeleted != null)
            {
                _unitOfWork.Repository<Brouchures>().Delete(brouchuresToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<BrouchuresVM>> GetAllAsync()
        {
            var brouchures = await _unitOfWork.Repository<Brouchures>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrouchuresVM>>(brouchures);
        }

        public async Task<BrouchuresVM?> GetByIdAsync(Guid id)
        {
            var brouchures = await _unitOfWork.Repository<Brouchures>().GetByIdAsync(id);
            return _mapper.Map<BrouchuresVM>(brouchures);
        }

        public async Task<int> UpdateAsync(BrouchuresVM entity)
        {
            var brouchuresToBeUpdated = _mapper.Map<Brouchures>(entity);
            _unitOfWork.Repository<Brouchures>().Update(brouchuresToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
