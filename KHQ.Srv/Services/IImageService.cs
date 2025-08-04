using KHQ.Domain;
using KHQ.Domain.Entities;

namespace KHQ.Srv.Services
{
    public interface IImageService
    {
        Task<List<Image>> GetImagesAsync(Guid foreignKey, ImageType imageType);
        Task<int> DeleteImagesAsync(Guid foreignKey);
    }

}
