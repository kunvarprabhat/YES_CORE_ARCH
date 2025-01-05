using YES.Dtos;
using YES.Dtos.Mail;

namespace YES.Services.ComunicationServices
{
    public interface ICommunication
    {
        Task<APIResponse> SendAsync(SendMailDto mailDto);
        Task<APIResponse> SendBulkAsync();
        Task<APIResponse> SendEmailAsync(SendMailDto model);
    }
}
