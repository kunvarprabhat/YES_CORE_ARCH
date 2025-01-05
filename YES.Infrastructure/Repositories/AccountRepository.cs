using Microsoft.AspNetCore.Identity;
using YES.Domain.Auth;
using YES.Infrastructure.IRepositories;
using System.Net;
using YES.Dtos;
using YES.Dtos.API;
using Microsoft.EntityFrameworkCore;
using YES.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using YES.Utility.Constants;
using YES.Utility.Resources;

namespace YES.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
          , SignInManager<ApplicationUser> signInManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }

        public async Task<APIResponse<Branch>> Login(ApplicationUser user)
        {
            var response = new APIResponse<Branch>();
            try
            {
                var userExist = await _userManager.FindByNameAsync(user?.UserName!);
                if (userExist == null)
                {
                    response.Message = string.Format(CommonResource.UserNotExist, user.UserName);
                    response.Status = HttpStatusCode.NotFound;
                    response.Success = false;
                    return await Task.FromResult(response);
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, lockoutOnFailure: false);

                    //var result = await _signInManager.CheckPasswordSignInAsync(userExist, user.Password, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var role = await _appDbContext.UserRoles.Where(x => x.UserId == userExist.Id).FirstOrDefaultAsync();
                        var roleName = await _appDbContext.Roles.Where(x => x.Id == role.RoleId).Select(x => x.Name).FirstOrDefaultAsync();
                        var data = await _appDbContext.Branches.Include(x => x.User).AsNoTracking().FirstAsync(x => Equals(x.UserId, userExist.Id));
                        var customClaims = new List<Claim> {
                            new Claim(ClaimConstants.UserId, data.User.Id.ToString()),
                            new Claim(ClaimConstants.RoleId, role.RoleId.ToString()),
                            new Claim(ClaimConstants.RoleName, roleName?.ToString()!),
                            new Claim(ClaimConstants.BranchId, data.Id.ToString()),
                            new Claim(ClaimConstants.BranchType, data.BranchType.ToString()),
                            new Claim(ClaimConstants.BranchType, data.BranchType.ToString()),
                            new Claim(ClaimConstants.CenterName, data.CenterName.ToString()),
                            new Claim(ClaimConstants.Name, data.User.FirstName+data.User.LastName.ToString()),
                            new Claim(ClaimConstants.ExpireDate,(DateTime.Now.AddMinutes(30)).ToString() ?? string.Empty),

                           };
                        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                        if (customClaims != null && claimsPrincipal?.Identity is ClaimsIdentity claimsIdentity)
                        {
                            claimsIdentity.AddClaims(customClaims);
                        }
                        await _signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme,
                            claimsPrincipal,
                            new AuthenticationProperties { IsPersistent = true });
                        response.Message = "User Login Successfuly";
                        response.Status = HttpStatusCode.OK;
                        response.Success = true;
                        response.Data = data;
                        return await Task.FromResult(response);
                    }
                    else
                    {
                        response.Message = "Password Mis match. Please check your password and try again";
                        response.Status = HttpStatusCode.Unauthorized;
                        response.Success = false;
                        return await Task.FromResult(response);

                    }

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
                response.Status = HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Error = new APIException(ex.Message, ex.InnerException);
            }
            return response;
        }

    }
}
