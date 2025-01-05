using YES.Dtos;
using YES.Dtos.Home;

namespace Yesscc.Services.IServices
{
    public interface IContactUsService
    {
        Task<APIResponse<ContactUsDto>> CreateAsync(ContactUsDto contactUsDto);
        Task<APIResponse<IEnumerable<ContactUsDto>>> GetaAllAsync();
    }
}
