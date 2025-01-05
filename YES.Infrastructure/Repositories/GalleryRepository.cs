using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;
using YES.Utility.Enums;
using YES.Utility.Resources;

namespace YES.Infrastructure.Repositories
{
    public class GalleryRepository : Repository<ImageGallery>, IGalleryRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        public GalleryRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext appContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = appContext;
        }

        public async Task<APIResponse<ImageGallery>> Create(ImageGallery imageGallery)
        {
            APIResponse<ImageGallery> apiResponse = new APIResponse<ImageGallery>();
            try
            {
                Add(imageGallery);
                var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return await Task.FromResult(apiResponse).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Error = new APIException(ex.Message, ex.InnerException);
                apiResponse.Message = CommonResource.AddFailed;
                apiResponse.Status = HttpStatusCode.InternalServerError;
                throw;
            }
        }

        public Task<APIResponse> Delete(int GId)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse> Edit(ImageGallery imageGallery)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ImageGallery>> GetAll(string type)
        {
            return await List(FilterByIsDeleted(type)).ToListAsync();
        }
        public async Task<IEnumerable<ImageGallery>> GetAll()
        {
            return await List(FilterByIsDeleted()).ToListAsync();
        }

        public async Task<ImageGallery> GetById(int GId)
        {
            return await List(FilterById(GId)).FirstAsync();
        }
        Expression<Func<ImageGallery, bool>> FilterByIsDeleted(string type)
        {
            return x => x.IsDeleted == false && x.IsActive && x.ImageType == type;
        }
        Expression<Func<ImageGallery, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false && x.IsActive == true && !Equals(x.ImageType, GalleryType.Slider.ToString());
        }

        Expression<Func<ImageGallery, bool>> FilterById(int Id)
        {
            return x => x.Id == Id;
        }
    }

}
