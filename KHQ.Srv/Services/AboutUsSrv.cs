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
    public class AboutUsSrv : IAboutUsSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AboutUsSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(AboutUsVM entity)
        {
            var aboutUsToBeAdded = _mapper.Map<AboutUs>(entity);
            await _unitOfWork.Repository<AboutUs>().AddAsync(aboutUsToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var aboutUsToBeDeleted = await _unitOfWork.Repository<AboutUs>().GetByIdAsync(id);
            if (aboutUsToBeDeleted != null)
            {
                _unitOfWork.Repository<AboutUs>().Delete(aboutUsToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<AboutUsVM>> GetAllAsync()
        {
            var aboutUs = await _unitOfWork.Repository<AboutUs>().GetAllAsync();
            return _mapper.Map<IEnumerable<AboutUsVM>>(aboutUs);
        }

        public async Task<AboutUsVM?> GetByIdAsync(Guid id)
        {
            var aboutUs = await _unitOfWork.Repository<AboutUs>().GetByIdAsync(id);
            return _mapper.Map<AboutUsVM>(aboutUs);
        }

        public async Task<int> UpdateAsync(AboutUsVM entity)
        {
            var aboutUsToBeUpdated = _mapper.Map<AboutUs>(entity);
            _unitOfWork.Repository<AboutUs>().Update(aboutUsToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
