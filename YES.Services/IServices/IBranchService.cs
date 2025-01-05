
using YES.Dtos;
using YES.Dtos.Branch;
using YES.Services.Interface;

namespace YES.Services.IServices
{
    public interface IBranchService : IUtilityService<BranchDto>
    {
        Task<APIResponse> UpdateStatustAsync(int id);
        Task<APIResponse<BranchDto>> GetBranchDetailsAsync(int id);
    }

}
