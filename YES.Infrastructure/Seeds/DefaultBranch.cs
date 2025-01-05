using YES.Domain.Entities;
using YES.Utility.Enum;
using YES.Utility.Enums;

namespace YES.Infrastructure.Seeds
{
    public static class DefaultBranchh
    {
        public static List<Branch> IdentityBranchList()
        {
            return new List<Branch>()
            {
                new Branch
                {
                    Id = 1,
                    IsActive = true,
                    CenterId = "MainCenter001",
                    CenterName = "YES",
                    IsVerify = true,
                    IsDeleted = false,
                    EmailId = "info@YES.in",
                    City = "Prayadraj",
                    Name = "Sonu",
                    LastName = "Kumar",
                    State = "Uttar Pradesh",
                    PhoneNo = "",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System",
                    UserId=Constants.SuperAdminUser,
                    BranchType=(int)BranchType.MainBranch
                },
                new Branch
                {
                    Id = 2,
                    IsActive = true,
                    CenterId ="YES20CID00001",
                    CenterName = "SHIV NARESH COMPUTER CENTER",
                    IsVerify = true,
                    IsDeleted = false,
                    EmailId = "parwej@YES.in",
                    City = "Prayadraj",
                    Name = "Parvez",
                    LastName = "Ansari",
                    State = "Uttar Pradesh",
                    PhoneNo = "8354021883",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System",
                    UserId=Constants.BranchAdmin,
                    BranchType=(int)BranchType.Other
                },
            };
        }
    }
}
