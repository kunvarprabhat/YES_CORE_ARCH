using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Dtos.API;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;
using YES.Utility.Common;
using YES.Utility.Resources;

namespace YES.Infrastructure.Repositories
{
    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        public RegistrationRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext appContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = appContext;
        }
        public async Task<APIResponse<Registration>> Create(Registration registration)
        {
            APIResponse<Registration> apiResponse = new APIResponse<Registration>();
            try
            {
                var data = _dbContext.Registrations.OrderByDescending(x => x.Id).FirstOrDefault();
                if (data != null)
                {
                    string digit = data.Id.ToString().GenrateRegNumber(data.Id + 1);
                    registration.RegistrationNo = "RID" + DateTime.Now.Year / 100 + "CC" + DateTime.Now.Year % 100 + digit;

                }
                else
                {
                    registration.RegistrationNo = "RID" + DateTime.Now.Year / 100 + "CC" + DateTime.Now.Year % 100 + "00001";
                }

                Add(registration);
                var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                apiResponse.Data = registration;
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

        public Task<APIResponse> Delete(int RId)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> Edit(Registration registration)
        {

            APIResponse apiResponse = new APIResponse();
            try
            {
                var record = await GetById(registration.Id);
                if (record != null)
                {
                    record.Name = registration.Name;
                    record.FatherName = registration.FatherName;
                    record.MotherName= registration.MotherName;
                    record.Address = registration.Address;
                    record.Dob = registration.Dob;
                    record.IdentityNo = registration.IdentityNo;
                    record.State = registration.State;
                    record.City = registration.City;
                    record.EmailId = registration.EmailId;
                    record.MobileNo = registration.MobileNo;
                    record.PinCode = registration.PinCode;
                    record.Gender = registration.Gender;
                }
                Update(record);
                var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                if (result > 0)
                {
                    apiResponse.Success = true;
                    apiResponse.Message = CommonResource.UpdateSuccess;
                    apiResponse.Status = HttpStatusCode.OK;
                }
                return await Task.FromResult(apiResponse).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Error = new APIException(ex.Message, ex.InnerException);
                apiResponse.Message = CommonResource.UpdateFailed;
                apiResponse.Status = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }

        public async Task<IEnumerable<Registration>> GetAll(int BranchId, int BranchType)
        {
            return await List(FilterByIsDeleted(BranchId, BranchType)).ToListAsync();
        }

        public async Task<Registration> GetById(int Id)
        {
            return await List(FilterById(Id)).FirstAsync();
        }


        public async Task<Registration> GetById(string RId)
        {
            return await List(FilterById(RId)).FirstAsync();
        }

        Expression<Func<Registration, bool>> FilterByIsDeleted(int BranchId, int BranchType)
        {
            return BranchType == 0 ? x => x.IsDeleted == false && x.IsCompleted == false :
                x => x.IsDeleted == false && x.IsCompleted == false && x.BranchId == BranchId;
        }
        Expression<Func<Registration, bool>> FilterById(int Id)
        {
            return x => x.Id == Id;
        }
        Expression<Func<Registration, bool>> FilterById(string RId)
        {
            return x => x.RegistrationNo == RId;
        }
    }
}
