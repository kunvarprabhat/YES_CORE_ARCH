using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using YES.Domain.Auth;
using YES.Domain.Entities;

namespace YES.Infrastructure.Seeds
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            CreateRoles(modelBuilder);
            CreateBasicUsers(modelBuilder);
            CreateBranch(modelBuilder);
            MapUserRole(modelBuilder);
        }

        private static void CreateRoles(ModelBuilder modelBuilder)
        {
            List<IdentityRole> roles = DefaultRoles.IdentityRoleList();
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

        private static void CreateBranch(ModelBuilder modelBuilder)
        {
            List<Branch> branch = DefaultBranchh.IdentityBranchList();
            modelBuilder.Entity<Branch>().HasData(branch);
        }
        private static void CreateBasicUsers(ModelBuilder modelBuilder)
        {
            List<ApplicationUser> users = DefaultUser.IdentityBasicUserList();
            modelBuilder.Entity<ApplicationUser>().HasData(users);
        }

        private static void MapUserRole(ModelBuilder modelBuilder)
        {
            var identityUserRoles = MappingUserRole.IdentityUserRoleList();
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(identityUserRoles);
        }
    }
}
