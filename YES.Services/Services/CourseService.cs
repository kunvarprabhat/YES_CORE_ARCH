using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.Course;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<APIResponse<CourseDto>> CreateAsync(List<CourseDto> courseDto)
        {
            APIResponse<CourseDto> response = new APIResponse<CourseDto>();

            try
            {
                var Model = _mapper.Map<IEnumerable<Course>>(courseDto);
                var courses = await _courseRepository.Create(Model);

                if (courses != null)
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

        public Task<APIResponse<CourseDto>> CreateAsync(CourseDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> DeleteAsync(CourseDto entity)
        {
            APIResponse response = new APIResponse();

            try
            {
                var Model = _mapper.Map<Course>(entity);
                var menus = await _courseRepository.DeleteRecord(Model);

                if (menus != null)
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

                throw;
            }
            return response;
        }


        public async Task<APIResponse<CourseDto>> EditAsync(CourseDto courseDto)
        {
            APIResponse<CourseDto> response = new APIResponse<CourseDto>();

            try
            {
                var Model = _mapper.Map<Course>(courseDto);
                var courses = await _courseRepository.Edit(Model);

                if (courses != null)
                {
                    response.Success = true;
                    response.Message = CommonResource.FetchSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Data = _mapper.Map<CourseDto>(courses);
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

        public async Task<APIResponse<IEnumerable<CourseDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<CourseDto>> response = new APIResponse<IEnumerable<CourseDto>>();

            try
            {
                var courses = await _courseRepository.GetAll();

                if (courses != null)
                {
                    response.Success = true;
                    response.Data = _mapper.Map<IEnumerable<CourseDto>>(courses);
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

        public async Task<APIResponse<CourseDto>> GetByIdAsync(int id)
        {
            APIResponse<CourseDto> response = new APIResponse<CourseDto>();
            var courses = await _courseRepository.GetById(id);

            if (courses != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<CourseDto>(courses);
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

        public async Task<APIResponse<CourseEnquiryDto>> GetCourseDetailsAsync(int CId)
        {
            APIResponse<CourseEnquiryDto> response = new APIResponse<CourseEnquiryDto>();
            var courses = await _courseRepository.GetCourseDetailsAsync(CId);

            if (courses != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<CourseEnquiryDto>(courses);
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
