
using YES.Dtos.Student;
using YES.Dtos;

namespace YES.Services.IServices
{
    public interface IStudentService
    {
        Task<APIResponse<StudentDto>> Create(StudentDto regitrationDto);
        Task<APIResponse<IEnumerable<StudentDto>>> GetAll(int BranchId, int BranchType);
        Task<APIResponse<StudentDto>> GetById(int SId);
        Task<APIResponse> Edit(StudentDto regitrationDto);
        Task<APIResponse> Delete(int SId);
        Task<APIResponse<StudentDto>> GetProfileById(int SId);
        Task<APIResponse> UploadProfileImage(StudentProfileImage profileImage);
        Task<APIResponse<StudentDto>> GetResultByRIdOrSId(string SId);
    }
}
