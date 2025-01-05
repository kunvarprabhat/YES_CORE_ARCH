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
    public class TopMessageRepository : Repository<TopOffer>, ITopMessageRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public TopMessageRepository(DbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<TopOffer>> Create(TopOffer entity)
        {
            var apiResponse = new APIResponse<TopOffer>();
            try
            {
                Add(entity);
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

        public async Task<int> DeleteRecord(TopOffer entity)
        {
            try
            {
                var record = await First(FilterById(entity.Id));
                record.Id = entity.Id;
                record.IsDeleted = !record.IsDeleted;
                Update(record);
                return await _unitOfWork.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> Edit(TopOffer entity)
        {

            var record = await First(FilterById(entity.Id));
            record.UpdatedBy = entity.UpdatedBy;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);

        }

        public async Task<IEnumerable<TopOffer>> GetAll()
        {
            return await List(FilterByIsDeleted()).ToListAsync();
        }

        public async Task<TopOffer> GetById(int Id)
        {
            return await First(FilterById(Id));
        }
        Expression<Func<TopOffer, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
        Expression<Func<TopOffer, bool>> FilterById(int Id)
        {
            return x => x.Id == Id;
        }
    }
}
