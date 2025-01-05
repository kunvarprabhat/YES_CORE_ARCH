
using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.TopMessage;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class TopMessageService : ITopMessageService
    {
        public readonly ITopMessageRepository _topMessageRepository;
        public readonly IMapper _mapper;
        public TopMessageService(ITopMessageRepository topMessageRepository, IMapper mapper)
        {
            _topMessageRepository = topMessageRepository;
            _mapper = mapper;
        }
        public async Task<APIResponse<TopMessageDto>> CreateAsync(TopMessageDto entity)
        {
            var response = new APIResponse<TopMessageDto>();

            try
            {
                var Model = _mapper.Map<TopOffer>(entity);
                var result = await _topMessageRepository.Create(Model);
                if (result != null)
                {
                    response.Success = true;
                    response.Message = CommonResource.AddSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = CommonResource.AddFailed;
                    response.Status = HttpStatusCode.InternalServerError;
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

        public async Task<APIResponse> DeleteAsync(TopMessageDto entity)
        {
            var response = new APIResponse();
            try
            {
                var Model = _mapper.Map<TopOffer>(entity);
                var result = await _topMessageRepository.DeleteRecord(Model);

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
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
                response.Message = CommonResource.DeleteFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public Task<APIResponse<TopMessageDto>> EditAsync(TopMessageDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse<IEnumerable<TopMessageDto>>> GetAllAsync()
        {
            var response = new APIResponse<IEnumerable<TopMessageDto>>();

            try
            {
                var result = await _topMessageRepository.GetAll();

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<TopMessageDto>>(result);
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

        public async Task<APIResponse<TopMessageDto>> GetByIdAsync(int id)
        {
            var response = new APIResponse<TopMessageDto>();

            try
            {
                var result = await _topMessageRepository.GetById(id);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<TopMessageDto>(result);
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
    }
}
