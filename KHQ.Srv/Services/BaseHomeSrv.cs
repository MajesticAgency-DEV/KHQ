using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;
using Microsoft.EntityFrameworkCore;

namespace KHQ.Srv.Services
{
    public class BaseHomeSrv : IBaseHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BaseHomeSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BaseHomeVM>> GetAllAsync()
        {
            var entities = await _unitOfWork.Repository<BaseHome>().GetAllAsync();
            return _mapper.Map<IEnumerable<BaseHomeVM>>(entities);
        }

        public async Task<BaseHomeVM?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.Repository<BaseHome>().GetByIdAsync(id);
            return entity != null ? _mapper.Map<BaseHomeVM>(entity) : null;
        }

        public async Task<int> AddAsync(BaseHomeVM dto)
        {
            var result = 0;
            var entity = _mapper.Map<BaseHome>(dto);
            await _unitOfWork.Repository<BaseHome>().AddAsync(entity);
            result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(BaseHomeVM dto)
        {
            var result = 0;
            var existing = await _unitOfWork.Repository<BaseHome>().GetByIdAsync(dto.Id);
            if (existing == null)
                throw new Exception("Record not found");

            var entity = _mapper.Map(dto, existing); // map into the existing entity
            _unitOfWork.Repository<BaseHome>().Update(entity);
            result = await _unitOfWork.SaveChangesAsync();
            return result;
        }


        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var entity = await _unitOfWork.Repository<BaseHome>().GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.Repository<BaseHome>().Delete(entity);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public IQueryable<BaseHome> Queryable()
        {
            return _unitOfWork.Repository<BaseHome>().Queryable();
        }


        public async Task<BaseHomeVM> GetSectionDataAsync(int sectionType)
        {
            var entity = await _unitOfWork.Repository<BaseHome>().Queryable().Where(x =>x.SectionType == sectionType).FirstOrDefaultAsync();

            if (entity == null) return null;

            return _mapper.Map<BaseHomeVM>(entity);
        }

        public async Task<int> SaveSectionAsync(BaseHomeVM request)
        {
            var entity = _mapper.Map<BaseHome>(request);

            if (request.Id == Guid.Empty)
            {
                // insert 
                await _unitOfWork.Repository<BaseHome>().AddAsync(entity);
            }
            else
            {
                // Update existing entity
                _unitOfWork.Repository<BaseHome>().Update(entity);
            }


            return await _unitOfWork.SaveChangesAsync();
        }
    }
}

