using AutoMapper;
using KHQ.Domain;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using KHQ.Srv.Services;

namespace KHQ.Portal.Service
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Image>> GetImagesAsync(Guid foreignKey, ImageType imageType)
        {
            var entities = await _unitOfWork.Repository<Image>().GetAllAsync();
            return entities.Where(x => x.F_Key == foreignKey && x.ImageType == imageType).ToList();
        }

        public async Task<List<Image>> GetImagesByImageTypeAsync(ImageType imageType)
        {
            var entities = await _unitOfWork.Repository<Image>().GetAllAsync();
            return entities.Where(x => x.ImageType == imageType).ToList();
        }
        public async Task<int> DeleteImagesAsync(Guid foreignKey)
        {
            var result = 0;
            var entities = await _unitOfWork.Repository<Image>()
                .GetWhereAsync(img => img.F_Key == foreignKey);
            
            if (entities != null)
            {
                foreach (var entity in entities) {
                    _unitOfWork.Repository<Image>().Delete(entity);
                    result = await _unitOfWork.SaveChangesAsync();
                }
            }
            return result;
        }
    }
}
