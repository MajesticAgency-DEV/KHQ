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
    public class WhyChooseUsSrv : IWhyChooseUsSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WhyChooseUsSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(WhyChooseUsVM entity)
        {
            var whyChooseUsToBeAdded = _mapper.Map<WhyChooseUs>(entity);
            await _unitOfWork.Repository<WhyChooseUs>().AddAsync(whyChooseUsToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var whyChooseUsToBeDeleted = await _unitOfWork.Repository<WhyChooseUs>().GetByIdAsync(id);
            if (whyChooseUsToBeDeleted != null)
            {
                await _unitOfWork.Repository<WhyChooseUs>().Delete(whyChooseUsToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<WhyChooseUsVM>> GetAllAsync()
        {
            var whyChooseUsData = await _unitOfWork.Repository<WhyChooseUs>().GetAllAsync();
            return _mapper.Map<IEnumerable<WhyChooseUsVM>>(whyChooseUsData);
        }

        public async Task<WhyChooseUsVM?> GetByIdAsync(Guid id)
        {
            var whyChooseUsData = await _unitOfWork.Repository<WhyChooseUs>().GetByIdAsync(id);
            return _mapper.Map<WhyChooseUsVM>(whyChooseUsData);
        }

        public async Task<int> UpdateAsync(WhyChooseUsVM entity)
        {
            var whyChooseUsToBeUpdated = _mapper.Map<WhyChooseUs>(entity);
            _unitOfWork.Repository<WhyChooseUs>().Update(whyChooseUsToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
