using YES.Domain.Entities;
using YES.Dtos;
namespace YES.Infrastructure.IRepositories
{
    public interface IStudentRepository
    {
        Task<APIResponse<Student>> Create(Student Student);
        Task<IEnumerable<Student>> GetAll(int BranchId, int BranchType);
        Task<Student> GetById(int SId);
        Task<APIResponse> Edit(Student Student);
        Task<APIResponse> Delete(int SId);
        Task<Student> GetProfileById(int SId);
        Task<int> UploadProfileImage(Student Student);
        Task<Student> GetResultByRIdOrSId(string SId);
    }
}
