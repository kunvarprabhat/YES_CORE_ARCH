using AutoMapper;
using System.Net;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.Gallery;
using YES.Infrastructure.IRepositories;
using YES.Utility.Resources;
using YES.Services.IServices;
using YES.Domain.Entities;

namespace YES.Services.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IMapper _mapper;
        public GalleryService(IGalleryRepository galleryRepository, IMapper mapper)
        {
            _galleryRepository = galleryRepository;
            _mapper = mapper;
        }


        public async Task<APIResponse<GalleryDto>> CreateAsync(GalleryDto entity)
        {
            APIResponse<GalleryDto> response = new APIResponse<GalleryDto>();

            try
            {
                var Model = _mapper.Map<ImageGallery>(entity);
                Model.IsActive = true;
                var result = await _galleryRepository.Create(Model);

                if (result != null)
                {
                    response.Success = true;
                    response.Message = CommonResource.FetchSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.FetchFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
                throw;
            }

            return response;
        }

        public Task<APIResponse> DeleteAsync(GalleryDto entity)
        {
            throw new NotImplementedException();
        }
        public Task<APIResponse<GalleryDto>> EditAsync(GalleryDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse<IEnumerable<GalleryDto>>> GetAll(string galleryType)
        {
            APIResponse<IEnumerable<GalleryDto>> response = new APIResponse<IEnumerable<GalleryDto>>();

            try
            {
                var result = await _galleryRepository.GetAll(galleryType);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<GalleryDto>>(result);
                    response.Message = CommonResource.FetchSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.FetchFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
                throw;
            }

            return response;
        }


        public async Task<APIResponse<IEnumerable<GalleryDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<GalleryDto>> response = new APIResponse<IEnumerable<GalleryDto>>();

            try
            {
                var result = await _galleryRepository.GetAll();

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<GalleryDto>>(result);
                    response.Message = CommonResource.FetchSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.FetchFailed;
                    response.Status = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
                throw;
            }

            return response;
        }

        public Task<APIResponse<GalleryDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
