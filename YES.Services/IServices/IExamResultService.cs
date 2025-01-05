using YES.Dtos;
using YES.Dtos.Result;
using YES.Services.Interface;

namespace YES.Services.IServices
{
    public interface IExamResultService : IUtilityService<ExamResultDto>
    {
        public Task<APIResponse<IEnumerable<ExamResultDto>>> GetAllByStudentId(int Id);
    }

}
