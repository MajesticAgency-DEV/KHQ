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
    public class SocialMediaSrv : ISocialMediaSrv
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SocialMediaSrv(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(SocialMediaVM entity)
        {
            var socialMediaToBeAdded = _mapper.Map<SocialMedia>(entity);
            await _unitOfWork.Repository<SocialMedia>().AddAsync(socialMediaToBeAdded);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = 0;
            var socialMediaToBeDeleted = await _unitOfWork.Repository<SocialMedia>().GetByIdAsync(id);
            if (socialMediaToBeDeleted != null)
            {
                _unitOfWork.Repository<SocialMedia>().Delete(socialMediaToBeDeleted);
                result = await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<SocialMediaVM>> GetAllAsync()
        {
            var socialMidia = await _unitOfWork.Repository<SocialMedia>().GetAllAsync();
            return _mapper.Map<IEnumerable<SocialMediaVM>>(socialMidia);
        }

        public async Task<SocialMediaVM?> GetByIdAsync(Guid id)
        {
            var socialMidia = await _unitOfWork.Repository<SocialMedia>().GetByIdAsync(id);
            return _mapper.Map<SocialMediaVM>(socialMidia);
        }

        public async Task<int> UpdateAsync(SocialMediaVM entity)
        {
            var socialMediaToBeUpdated = _mapper.Map<SocialMedia>(entity);
            _unitOfWork.Repository<SocialMedia>().Update(socialMediaToBeUpdated);
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
