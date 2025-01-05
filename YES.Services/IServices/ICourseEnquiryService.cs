using YES.Dtos;
using YES.Dtos.Course;

namespace YES.Services.IServices
{
    public interface ICourseEnquiryService
    {
        Task<APIResponse<CourseEnquiryDto>> CreateAsync(CourseEnquiryDto entity);
        Task<APIResponse<IEnumerable<CourseEnquiryDto>>> GetAllAsync();
    }
}
