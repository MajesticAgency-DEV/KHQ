using AutoMapper;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Repo.UOW;

namespace KHQ.Srv.Services
{
    public class SliderSrv : ISliderSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SliderSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(SliderVM entity)
        {
            var sliderToBeAdded = _mapper.Map<Slider>(entity);
            await _unitOfWork.Repository<Slider>().AddAsync(sliderToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var sliderToBeDeleted = await _unitOfWork.Repository<Slider>().GetByIdAsync(id);
            if (sliderToBeDeleted != null)
            {
                _unitOfWork.Repository<Slider>().Delete(sliderToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<SliderVM>> GetAllAsync()
        {
            var sliders = await _unitOfWork.Repository<Slider>().GetAllAsync();
            return _mapper.Map<IEnumerable<SliderVM>>(sliders);
        }

        public async Task<SliderVM?> GetByIdAsync(Guid id)
        {
            var slider = await _unitOfWork.Repository<Slider>().GetByIdAsync(id);
            return _mapper.Map<SliderVM>(slider);
        }

        public async Task<int> UpdateAsync(SliderVM entity)
        {
            var sliderToBeUpdated = _mapper.Map<Slider>(entity);
            _unitOfWork.Repository<Slider>().Update(sliderToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
