using YES.Dtos;
using YES.Dtos.Result;

namespace YES.Services.IServices
{
    public interface IResultCertificateService
    {
        Task<APIResponse> UpdateCertificateIssueDate(ResultCertificateDto resultCertificateDto);
        Task<APIResponse> UpdateMarksheetIssueDate(ResultCertificateDto resultCertificateDto);
    }
}
