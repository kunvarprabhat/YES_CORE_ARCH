using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;

namespace YES.Infrastructure.Repositories
{
    public class SyllabusRepository : Repository<SyllabusDetail>, ISyllabusRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        public SyllabusRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext dbContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<APIResponse<SyllabusDetail>> Create(SyllabusDetail entity)
        {
            APIResponse<SyllabusDetail> response = new APIResponse<SyllabusDetail>();

            Add(entity);
            var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
            response.Data = entity;
            return await Task.FromResult(response).ConfigureAwait(false);
        }

        public async Task<int> DeleteRecord(SyllabusDetail entity)
        {
            var record = await First(FilterById(entity.Id));
            record.IsDeleted = !record.IsDeleted;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task<int> Edit(SyllabusDetail entity)
        {
            var record = await First(FilterById(entity.Id));
            record.Heading = entity.Heading;
            record.Description = entity.Description;
            record.UpdatedBy = entity.UpdatedBy;
            record.CourseId = entity.CourseId;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<SyllabusDetail>> GetAll()
        {
            return await _dbContext.SyllabusDetails.Include(x => x.Course).Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<SyllabusDetail> GetById(int Id)
        {
            return await First(FilterById(Id));
        }
        Expression<Func<SyllabusDetail, bool>> FilterById(int Id)
        {
            return x => x.Id == Id && !x.IsDeleted;
        }
    }
}
