using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class CategorySrv : ICategorySrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategorySrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(CategoryVM entity)
        {
            var categoryToBeAdded = _mapper.Map<Category>(entity);
            await _unitOfWork.Repository<Category>().AddAsync(categoryToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var categoryToBeDeleted = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (categoryToBeDeleted != null)
            {
                _unitOfWork.Repository<Category>().Delete(categoryToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var h_AboutUs = await _unitOfWork.Repository<Category>().GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryVM>>(h_AboutUs);
        }

        public async Task<CategoryVM?> GetByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            return _mapper.Map<CategoryVM>(category);
        }

        public async Task<int> UpdateAsync(CategoryVM entity)
        {
            var categoryToBeUpdated = _mapper.Map<Category>(entity);
            _unitOfWork.Repository<Category>().Update(categoryToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
