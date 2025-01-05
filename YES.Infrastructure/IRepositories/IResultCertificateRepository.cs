using YES.Domain.Entities;
using YES.Dtos;

namespace YES.Infrastructure.IRepositories
{
    public interface IResultCertificateRepository
    {
        public Task<APIResponse> UpdateCertificateIssueDate(ResultCertificate entity);
        public Task<APIResponse> UpdateMarksheetIssueDate(ResultCertificate entity);
    }
}
