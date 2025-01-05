using Microsoft.AspNetCore.Identity;
using YES.Utility.Enum;
using System.Collections.Generic;

namespace YES.Infrastructure.Seeds
{
    public static class MappingUserRole
    {
        public static List<IdentityUserRole<string>> IdentityUserRoleList()
        {
            return new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = Constants.BranchAdmin,
                    UserId = Constants.BranchAdmin
                },
                new IdentityUserRole<string>
                {
                    RoleId = Constants.SuperAdmin,
                    UserId = Constants.SuperAdminUser
                },
            };
        }
    }
}
