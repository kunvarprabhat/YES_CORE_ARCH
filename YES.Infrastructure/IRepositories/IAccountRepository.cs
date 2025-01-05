using YES.Domain.Auth;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.Account;

namespace YES.Infrastructure.IRepositories
{
    public interface IAccountRepository
    {
        //Task<APIResponse<UserInformationDTO>> RegisterNewUser(UserInformationDTO userInformation); 
        //Task<APIResponse<string>> ConfirmEmail(string userId, string code);
        Task<APIResponse<Branch>> Login(ApplicationUser user);
    }
}
