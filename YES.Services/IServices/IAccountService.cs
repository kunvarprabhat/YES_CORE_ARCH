using YES.Dtos;
using YES.Dtos.Account;
using YES.Dtos.Branch;

namespace YES.Services.IServices
{
    public interface IAccountService
    {
        Task<APIResponse<BranchDto>> Login(LoginDto loginDto);

    }
}
