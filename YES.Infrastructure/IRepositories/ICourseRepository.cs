using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.UnitOfWork.Interfaces;

namespace YES.Infrastructure.IRepositories
{
    public interface ICourseRepository : IUtilityRepository<Course>
    {
        Task<APIResponse<Course>> Create(IEnumerable<Course> course);
        Task<Course> GetCourseDetailsAsync(int Id);        

    }
}
