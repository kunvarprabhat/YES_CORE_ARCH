using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.Batches;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class BatchesService : IBatchesService
    {
        private readonly IBatchesRepository _batchesRepository;
        private readonly IMapper _mapper;

        public BatchesService(IBatchesRepository batchesRepository, IMapper mapper)
        {
            _batchesRepository = batchesRepository;
            _mapper = mapper;
        }

        public async Task<APIResponse<BatchDto>> CreateAsync(BatchDto entity)
        {
            APIResponse<BatchDto> response = new APIResponse<BatchDto>();
            var Model = _mapper.Map<BatchDetails>(entity);
            var result = await _batchesRepository.Create(Model);

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<BatchDto>(result.Data);
                response.Message = CommonResource.AddSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.AddFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<APIResponse> DeleteAsync(BatchDto entity)
        {
            APIResponse response = new APIResponse();
            var Model = _mapper.Map<BatchDetails>(entity);
            var result = await _batchesRepository.DeleteRecord(Model);

            if (result != 0)
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

            return response;
        }

        public async Task<APIResponse<BatchDto>> EditAsync(BatchDto entity)
        {
            APIResponse<BatchDto> response = new APIResponse<BatchDto>();
            var Model = _mapper.Map<BatchDetails>(entity);
            var result = await _batchesRepository.Edit(Model);

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

        public async Task<APIResponse<IEnumerable<BatchDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<BatchDto>> response = new APIResponse<IEnumerable<BatchDto>>();
            var result = await _batchesRepository.GetAll();

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<IEnumerable<BatchDto>>(result);
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

        public async Task<APIResponse<BatchDto>> GetByIdAsync(int id)
        {
            APIResponse<BatchDto> response = new APIResponse<BatchDto>();
            var result = await _batchesRepository.GetById(id);

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<BatchDto>(result);
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
