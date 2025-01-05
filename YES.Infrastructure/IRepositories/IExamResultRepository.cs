using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.UnitOfWork.Interfaces;

namespace YES.Infrastructure.IRepositories
{
    public interface IExamResultRepository : IUtilityRepository<ExamResult>
    {
        public Task<IEnumerable<ExamResult>> GetAllByStudentId(int Id);
    }
}
