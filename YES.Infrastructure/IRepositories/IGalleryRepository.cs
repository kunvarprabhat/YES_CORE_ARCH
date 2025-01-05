using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YES.Domain.Entities;
using YES.Dtos;

namespace YES.Infrastructure.IRepositories
{
    public interface IGalleryRepository
    {
        Task<APIResponse<ImageGallery>> Create(ImageGallery imageGallery);
        Task<IEnumerable<ImageGallery>> GetAll(string type);
        Task<IEnumerable<ImageGallery>> GetAll();
        Task<ImageGallery> GetById(int GId);
        Task<APIResponse> Edit(ImageGallery imageGallery);
        Task<APIResponse> Delete(int GId);
    }
}
