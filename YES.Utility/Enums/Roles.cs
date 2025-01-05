using System;

namespace YES.Utility.Enum
{
    public enum Roles
    {
        SuperAdmin,
        Admin,
        BranchAdmin,
        Basic,
        Employee
    }

    public static class Constants
    {
        public static readonly string SuperAdmin = Guid.NewGuid().ToString();
        public static readonly string Admin = Guid.NewGuid().ToString();
        public static readonly string BranchAdmin = Guid.NewGuid().ToString();
        public static readonly string Basic = Guid.NewGuid().ToString();
        public static readonly string Employee = Guid.NewGuid().ToString();

        public static readonly string SuperAdminUser = Guid.NewGuid().ToString();
        public static readonly string BasicUser = Guid.NewGuid().ToString();
    }


}
