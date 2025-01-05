using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.Result;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class ExamResultService : IExamResultService
    {
        private readonly IExamResultRepository _examResultRepository;
        private readonly IMapper _mapper;
        public ExamResultService(IExamResultRepository examResultRepository, IMapper mapper)
        {
            _examResultRepository = examResultRepository;
            _mapper = mapper;
        }
        public async Task<APIResponse<ExamResultDto>> CreateAsync(ExamResultDto entity)
        {
            APIResponse<ExamResultDto> response = new APIResponse<ExamResultDto>();

            try
            {
                var Model = _mapper.Map<ExamResult>(entity);
                var result = await _examResultRepository.Create(Model);

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

        public async Task<APIResponse> DeleteAsync(ExamResultDto entity)
        {
            APIResponse response = new APIResponse();
            try
            {
                var Model = _mapper.Map<ExamResult>(entity);
                var result = await _examResultRepository.DeleteRecord(Model);

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

        public async Task<APIResponse<ExamResultDto>> EditAsync(ExamResultDto entity)
        {
            APIResponse<ExamResultDto> response = new APIResponse<ExamResultDto>();
            var Model = _mapper.Map<ExamResult>(entity);
            var result = await _examResultRepository.Edit(Model);

            if (result != 0)
            {
                response.Success = true;
                response.Message = CommonResource.UpdateSuccess;
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = CommonResource.UpdateFailed;
                response.Status = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<APIResponse<IEnumerable<ExamResultDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<ExamResultDto>> response = new APIResponse<IEnumerable<ExamResultDto>>();

            try
            {
                var result = await _examResultRepository.GetAll();

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<ExamResultDto>>(result);
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

        public async Task<APIResponse<ExamResultDto>> GetByIdAsync(int id)
        {
            APIResponse<ExamResultDto> response = new APIResponse<ExamResultDto>();
            var result = await _examResultRepository.GetById(id);

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<ExamResultDto>(result);
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

        public async Task<APIResponse<IEnumerable<ExamResultDto>>> GetAllByStudentId(int Id)
        {
            APIResponse<IEnumerable<ExamResultDto>> response = new APIResponse<IEnumerable<ExamResultDto>>();

            try
            {
                var result = await _examResultRepository.GetAllByStudentId(Id);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<ExamResultDto>>(result);
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
