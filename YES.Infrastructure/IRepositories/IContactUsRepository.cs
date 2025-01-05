using YES.Domain.Entities;
using YES.Dtos;

namespace YES.Infrastructure.IRepositories
{
    public interface IContactUsRepository
    {
        Task<APIResponse<ContactUs>> Create(ContactUs contactUs);
        Task<IEnumerable<ContactUs>> GetAllCourse();
    }
}
