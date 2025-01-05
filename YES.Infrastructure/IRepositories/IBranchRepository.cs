using YES.Domain.Entities; 
using YES.Infrastructure.UnitOfWork.Interfaces;

namespace YES.Infrastructure.IRepositories
{
    public interface IBranchRepository : IUtilityRepository<Branch>
    {
        public Task<int> UpdateStatus(int Id);
        public Task<Branch> GetBranchDetails(int Id);
    }
}
