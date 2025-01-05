
using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.UnitOfWork.Interfaces;

namespace YES.Infrastructure.IRepositories
{
    public interface IExamRepository : IUtilityRepository<Exam>
    {
        Task<APIResponse<Exam>> Create(List<Exam> entity);
        Task<IEnumerable<Exam>> GetAllByCourseId(int courseId);
    }
}
