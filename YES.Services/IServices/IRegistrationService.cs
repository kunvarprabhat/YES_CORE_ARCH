using YES.Dtos;
using YES.Dtos.Student;

namespace YES.Services.IServices
{
    public interface IRegistrationService
    {
        Task<APIResponse<RegistrationDto>> Create(RegistrationDto regitrationDto);
        Task<APIResponse<IEnumerable<RegistrationDto>>> GetAll(int BranchId, int BranchType);
        Task<APIResponse<RegistrationDto>> GetById(int RId);
        Task<APIResponse<RegistrationDto>> GetById(string RegistrationNo);
        Task<APIResponse> Edit(RegistrationDto regitrationDto);
        Task<APIResponse> Delete(int RId);

    }
}
