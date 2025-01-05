using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.ExamResult;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        public ExamService(IExamRepository examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }
        public async Task<APIResponse<ExamDto>> CreateAsync(ExamDto entity)
        {
            APIResponse<ExamDto> response = new APIResponse<ExamDto>();

            try
            {
                var Model = _mapper.Map<Exam>(entity);
                var result = await _examRepository.Create(Model);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<ExamDto>(result);
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

        public async Task<APIResponse<ExamDto>> CreateAsync(List<ExamDto> entity)
        {
            APIResponse<ExamDto> response = new APIResponse<ExamDto>();

            try
            {
                var Model = _mapper.Map<IEnumerable<Exam>>(entity);
                var result = await _examRepository.Create(Model.ToList());

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<ExamDto>(result.Data);
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
            }

            return response;
        }

        public async Task<APIResponse> DeleteAsync(ExamDto entity)
        {
            APIResponse response = new APIResponse();
            var Model = _mapper.Map<Exam>(entity);
            var result = await _examRepository.DeleteRecord(Model);

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

        public async Task<APIResponse<ExamDto>> EditAsync(ExamDto entity)
        {
            APIResponse<ExamDto> response = new APIResponse<ExamDto>();

            var Model = _mapper.Map<Exam>(entity);
            var result = await _examRepository.Edit(Model);

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

        public async Task<APIResponse<IEnumerable<ExamDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<ExamDto>> response = new APIResponse<IEnumerable<ExamDto>>();

            try
            {
                var result = await _examRepository.GetAll();

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<ExamDto>>(result);
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
        public async Task<APIResponse<IEnumerable<ExamDto>>> GetAllByCourseIdAsync(int courseId)
        {
            APIResponse<IEnumerable<ExamDto>> response = new APIResponse<IEnumerable<ExamDto>>();

            try
            {
                var result = await _examRepository.GetAllByCourseId(courseId);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<ExamDto>>(result);
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

        public async Task<APIResponse<ExamDto>> GetByIdAsync(int id)
        {
            APIResponse<ExamDto> response = new APIResponse<ExamDto>();

            try
            {
                var result = await _examRepository.GetById(id);

                if (result != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<ExamDto>(result);
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
