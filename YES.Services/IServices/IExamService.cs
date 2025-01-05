
using YES.Dtos;
using YES.Dtos.ExamResult;
using YES.Services.Interface;

namespace YES.Services.IServices
{
    public interface IExamService : IUtilityService<ExamDto>
    {
        public Task<APIResponse<ExamDto>> CreateAsync(List<ExamDto> entity);
        public Task<APIResponse<IEnumerable<ExamDto>>> GetAllByCourseIdAsync(int courseId);
    }
}
