using Microsoft.AspNet.Identity;
using System.Security.Claims;
using YES.Comman.Interfaces;
using YES.Utility.Constants;

namespace YES.Comman
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimConstants.UserId)!;
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimConstants.Name)!;
            CenterName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimConstants.CenterName)!;
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
            RoleId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimConstants.RoleId)!;
            RoleName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimConstants.RoleName)!;
            BranchId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimConstants.BranchId))!;
            BranchType = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimConstants.BranchType))!;

        }
        public string UserId { get; }
        public string Name { get; }
        public string UserName { get; }
        public string CenterName { get; }

        public string RoleId { get; }

        public string RoleName { get; }

        public int BranchId { get; }

        public int BranchType { get; }
    }
}
