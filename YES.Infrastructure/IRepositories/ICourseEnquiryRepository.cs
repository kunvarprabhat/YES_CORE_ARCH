
using YES.Domain.Entities;
using YES.Dtos;

namespace YES.Infrastructure.IRepositories
{
    public interface ICourseEnquiryRepository
    {
        Task<APIResponse<CourseEnquiry>> Create(CourseEnquiry entity);
        Task<IEnumerable<CourseEnquiry>> GetAll();
    }
}
