using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;

namespace YES.Infrastructure.Repositories
{
    public class BatchesRepository : Repository<BatchDetails>, IBatchesRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        public BatchesRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext dbContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        public async Task<APIResponse<BatchDetails>> Create(BatchDetails entity)
        {
            APIResponse<BatchDetails> response = new APIResponse<BatchDetails>();
            Add(entity);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);
            response.Data = entity;
            return await Task.FromResult(response).ConfigureAwait(false);
        }

        public async Task<int> DeleteRecord(BatchDetails entity)
        {
            var record = await First(FilterById(entity.Id));
            record.IsDeleted = !record.IsDeleted;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task<int> Edit(BatchDetails entity)
        {
            var record = await First(FilterById(entity.Id));
            record.UpcomingBatch = entity.UpcomingBatch;
            record.Size = entity.Size;
            record.UpdatedBy = entity.UpdatedBy;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<BatchDetails>> GetAll()
        {
            return await _dbContext.BatchDetails.Include(x => x.Course).Where(x=>!x.IsDeleted).ToListAsync(); ;
        }

        public async Task<BatchDetails> GetById(int Id)
        {
            return await First(FilterById(Id));
        }
        Expression<Func<BatchDetails, bool>> FilterById(int Id)
        {
            return x => x.Id == Id && !x.IsDeleted;
        }
        Expression<Func<BatchDetails, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
    }
}
