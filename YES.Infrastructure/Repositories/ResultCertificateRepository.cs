using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net; 
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;
using YES.Utility.Resources;

namespace YES.Infrastructure.Repositories
{

    public class ResultCertificateRepository : Repository<ResultCertificate>, IResultCertificateRepository
    {
        public readonly IUnitOfWork _unitOfWork;
        public ResultCertificateRepository(DbFactory dbFactory, IUnitOfWork unitOfWork
            ) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse> UpdateCertificateIssueDate(ResultCertificate entity)
        {
            var response = new APIResponse();
            try
            {
                var Exist = await GetById(entity.StudentId);
                if (Exist != null)
                {
                    Exist.CertificateIssueDate = entity.CertificateIssueDate;
                    Update(Exist);
                    response.Success = true;
                    response.Message = CommonResource.UpdateSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    Add(entity);
                    response.Success = true;
                    response.Message = CommonResource.AddSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                await _unitOfWork.CommitAsync().ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = CommonResource.AddFailed;
                response.Status = HttpStatusCode.InternalServerError;
                response.Error = new APIException(ex.Message, ex.InnerException);
            }


            return response;
        }
        public async Task<APIResponse> UpdateMarksheetIssueDate(ResultCertificate entity)
        {
            var response = new APIResponse();
            try
            {
                var Exist = await GetById(entity.StudentId);
                if (Exist != null)
                {
                    Exist.MarksheetIssueDate = entity.MarksheetIssueDate;
                    Update(Exist);
                    response.Success = true;
                    response.Message = CommonResource.UpdateSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                else
                {
                    Add(entity);
                    response.Success = true;
                    response.Message = CommonResource.AddSuccess;
                    response.Status = HttpStatusCode.OK;
                }
                await _unitOfWork.CommitAsync().ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = CommonResource.AddFailed;
                response.Status = HttpStatusCode.InternalServerError;
                response.Error = new APIException(ex.Message, ex.InnerException);
            }


            return response;
        }
        public async Task<ResultCertificate> GetById(int Id)
        {
            return await List(FilterById(Id)).FirstOrDefaultAsync();
        }
        Expression<Func<ResultCertificate, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
        Expression<Func<ResultCertificate, bool>> FilterById(int studentId)
        {
            return x => x.StudentId == studentId;
        }


    }
}
