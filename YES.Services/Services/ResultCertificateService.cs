using AutoMapper;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Dtos.Branch;
using YES.Dtos.Result;
using YES.Infrastructure.IRepositories;
using YES.Services.IServices;
using YES.Utility.Resources;

namespace YES.Services.Services
{
    public class ResultCertificateService : IResultCertificateService
    {
        public readonly IMapper _mapper;
        public readonly IResultCertificateRepository _resultCertificate;
        public ResultCertificateService(IMapper mapper, IResultCertificateRepository resultCertificate)
        {
            _mapper = mapper;
            _resultCertificate = resultCertificate;
        }
        public async Task<APIResponse> UpdateCertificateIssueDate(ResultCertificateDto entity)
        {
            try
            {
                var Model = _mapper.Map<ResultCertificate>(entity);
                return await _resultCertificate.UpdateCertificateIssueDate(Model);
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = CommonResource.AddFailed,
                    Error = new APIException(ex.Message, ex.InnerException),
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<APIResponse> UpdateMarksheetIssueDate(ResultCertificateDto entity)
        {
            try
            {
                var Model = _mapper.Map<ResultCertificate>(entity);
                return await _resultCertificate.UpdateMarksheetIssueDate(Model);
            }
            catch (Exception ex)
            {
                return new APIResponse
                {
                    Success = false,
                    Message = CommonResource.AddFailed,
                    Error = new APIException(ex.Message, ex.InnerException),
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
