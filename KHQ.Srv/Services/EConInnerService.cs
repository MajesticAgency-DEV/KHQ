using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class EConInnerService : IEConInnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EConInnerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }        

        public async Task<IEnumerable<E_Con_InnerVM>> GetAllAsync()
        {
            var e_Con_Inners =  await _unitOfWork.Repository<E_Con_Inner>().GetAllAsync();
            return _mapper.Map<IEnumerable<E_Con_InnerVM>>(e_Con_Inners);
        }

        public async Task<E_Con_InnerVM?> GetByIdAsync(Guid id)
        {
            var e_ConInner = await _unitOfWork.Repository<E_Con_Inner>().GetByIdAsync(id);
            return _mapper.Map<E_Con_InnerVM>(e_ConInner);
        }

        public async Task<int> AddAsync(E_Con_InnerVM entity)
        {
            var e_ConInnerToBeAdded = _mapper.Map<E_Con_Inner>(entity);
            await _unitOfWork.Repository<E_Con_Inner>().AddAsync(e_ConInnerToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(E_Con_InnerVM entity)
        {
            var e_ConInnerToBeUpdated = _mapper.Map<E_Con_Inner>(entity);
            _unitOfWork.Repository<E_Con_Inner>().Update(e_ConInnerToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var entity = await _unitOfWork.Repository<E_Con_Inner>().GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.Repository<E_Con_Inner>().Delete(entity);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }

}
