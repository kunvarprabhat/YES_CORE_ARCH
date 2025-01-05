namespace YES.Comman.Interfaces
{
    public interface ICurrentUserService
    {
        string Name { get; }
        string UserId { get; }
        string UserName { get; }
        string RoleId { get; }
        string RoleName { get; }
        string CenterName { get; }
        int BranchId { get; }
        int BranchType { get; }
    }
}
