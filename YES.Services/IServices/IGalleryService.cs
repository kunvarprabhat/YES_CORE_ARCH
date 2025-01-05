using YES.Dtos;
using YES.Dtos.Gallery;
using YES.Services.Interface;

namespace YES.Services.IServices
{
    public interface IGalleryService : IUtilityService<GalleryDto>
    {
        Task<APIResponse<IEnumerable<GalleryDto>>> GetAll(string galleryType);

    }
}
