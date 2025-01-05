using Microsoft.AspNetCore.Identity;
using YES.Domain.Auth;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Repository;
using YES.Utility.Enum;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.UnitOfWork.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using YES.Dtos.API;
using YES.Utility.Resources;

namespace YES.Infrastructure.Repositories
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactUsRepository(DbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<ContactUs>> Create(ContactUs contactUs)
        {
            APIResponse<ContactUs> apiResponse = new APIResponse<ContactUs>();
            try
            {
                Add(contactUs);
                var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return await Task.FromResult(apiResponse).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Error = new APIException(ex.Message, ex.InnerException);
                apiResponse.Message = CommonResource.AddFailed;
                apiResponse.Status = HttpStatusCode.InternalServerError;
                throw;
            }
        }
        public async Task<IEnumerable<ContactUs>> GetAllCourse()
        {
            return await List(FilterByIsDeleted()).ToListAsync();
        }
        Expression<Func<ContactUs, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
    }
}
