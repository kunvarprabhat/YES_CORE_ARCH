using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.News;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public NewsService(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }

        public async Task<APIResponse<NewsDto>> CreateAsync(NewsDto entity)
        {
            var response = new APIResponse<NewsDto>();

            try
            {
                var Model = _mapper.Map<News>(entity);
                var result = await _newsRepository.Create(Model);

                if (result.Data != null)
                {
                    response.Data = _mapper.Map<NewsDto>(result.Data);
                    response.Message = result.Message;
                    response.Success = result.Success;
                    response.Status = result.Status;
                }
                else
                {
                    response.Success = true;
                    response.Message = CommonResource.AddSuccess;
                    response.Status = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.AddFailed;
                response.Status = HttpStatusCode.InternalServerError;
                throw;
            }

            return response;
        }

        public async Task<APIResponse> DeleteAsync(NewsDto entity)
        {
            APIResponse response = new APIResponse();
            var Model = _mapper.Map<News>(entity);
            var result = await _newsRepository.DeleteRecord(Model);

            if (result != 0)
            {
                response.Success = true;
                response.Message = CommonResource.DeleteSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.DeleteFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<NewsDto>> EditAsync(NewsDto entity)
        {
            APIResponse<NewsDto> response = new APIResponse<NewsDto>();
            var Model = _mapper.Map<News>(entity);
            var result = await _newsRepository.Edit(Model);

            if (result != 0)
            {
                response.Success = true;
                response.Message = CommonResource.UpdateSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Data = entity;
                response.Message = CommonResource.UpdateFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<IEnumerable<NewsDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<NewsDto>> response = new APIResponse<IEnumerable<NewsDto>>();
            var result = await _newsRepository.GetAll();

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<IEnumerable<NewsDto>>(result);
                response.Message = CommonResource.FetchSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponse<NewsDto>> GetByIdAsync(int id)
        {
            APIResponse<NewsDto> response = new APIResponse<NewsDto>();
            var result = await _newsRepository.GetById(id);

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<NewsDto>(result);
                response.Message = CommonResource.FetchSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.FetchFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
