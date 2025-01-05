using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.Course;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class CourseEnquiryService : ICourseEnquiryService
    {
        private readonly ICourseEnquiryRepository _courseEnquiryRepository;
        private readonly IMapper _mapper;

        public CourseEnquiryService(ICourseEnquiryRepository courseEnquiryRepository, IMapper mapper)
        {
            _courseEnquiryRepository = courseEnquiryRepository;
            _mapper = mapper;
        }

        public async Task<APIResponse<CourseEnquiryDto>> CreateAsync(CourseEnquiryDto entity)
        {
            APIResponse<CourseEnquiryDto> response = new APIResponse<CourseEnquiryDto>();
            var Model = _mapper.Map<CourseEnquiry>(entity);
            var result = await _courseEnquiryRepository.Create(Model);

            if (result != null && string.IsNullOrEmpty(result.Message))
            {
                response.Success = true;
                response.Data = _mapper.Map<CourseEnquiryDto>(result.Data);
                response.Message = "Your set book successfully. We are cotactu as soon as posiable!!";
                response.Status = HttpStatusCode.OK;
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
                response.Status = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<APIResponse<IEnumerable<CourseEnquiryDto>>> GetAllAsync()
        {
            APIResponse<IEnumerable<CourseEnquiryDto>> response = new APIResponse<IEnumerable<CourseEnquiryDto>>();
            var result = await _courseEnquiryRepository.GetAll();

            if (result != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<IEnumerable<CourseEnquiryDto>>(result);
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
