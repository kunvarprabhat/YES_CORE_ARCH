using YES.Dtos;
using YES.Dtos.Course;
using YES.Services.Interface;

namespace YES.Services.IServices
{
    public interface ICourseService : IUtilityService<CourseDto>
    {
        Task<APIResponse<CourseDto>> CreateAsync(List<CourseDto> courseDto);
        Task<APIResponse<CourseEnquiryDto>> GetCourseDetailsAsync(int CId);
    }
}
