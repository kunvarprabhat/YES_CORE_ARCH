using YES.Domain.Entities;
using YES.Dtos;

namespace YES.Infrastructure.IRepositories
{
    public interface IRegistrationRepository
    {
        Task<APIResponse<Registration>> Create(Registration registration);
        Task<IEnumerable<Registration>> GetAll(int BranchId, int BranchType);
        Task<Registration> GetById(int Id);
        Task<Registration> GetById(string RId);
        Task<APIResponse> Edit(Registration registration);
        Task<APIResponse> Delete(int RId);
    }
}
