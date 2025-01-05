using Microsoft.AspNetCore.Identity;
using YES.Utility.Enum;
using System.Collections.Generic;

namespace YES.Infrastructure.Seeds
{

    public static class DefaultRoles
    {
        public static List<IdentityRole> IdentityRoleList()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = Constants.SuperAdmin,
                    Name = Roles.SuperAdmin.ToString(),
                    NormalizedName = Roles.SuperAdmin.ToString()
                },
                new IdentityRole
                {
                    Id = Constants.Admin,
                    Name = Roles.Admin.ToString(),
                    NormalizedName = Roles.Admin.ToString()
                },
                new IdentityRole
                {
                    Id = Constants.BranchAdmin,
                    Name = Roles.BranchAdmin.ToString(),
                    NormalizedName = Roles.BranchAdmin.ToString()
                },
                new IdentityRole
                {
                    Id = Constants.Basic,
                    Name = Roles.Basic.ToString(),
                    NormalizedName = Roles.Basic.ToString()
                },
                 new IdentityRole
                {
                    Id = Constants.Employee,
                    Name = Roles.Employee.ToString(),
                    NormalizedName = Roles.Employee.ToString()
                }
            };
        }
    }
}
