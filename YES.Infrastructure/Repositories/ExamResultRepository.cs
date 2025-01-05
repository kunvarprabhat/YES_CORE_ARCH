using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;
namespace YES.Infrastructure.Repositories
{
    public class ExamResultRepository : Repository<ExamResult>, IExamResultRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        public ExamResultRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext dbContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<APIResponse<ExamResult>> Create(ExamResult entity)
        {
            APIResponse<ExamResult> response = new APIResponse<ExamResult>();
            Add(entity);
            var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
            return await Task.FromResult(response).ConfigureAwait(false);
        }

        public async Task<int> DeleteRecord(ExamResult entity)
        {
            var record = await First(FilterById(entity.Id));
            record.Id = entity.Id;
            record.IsDeleted = !record.IsDeleted;
            _dbContext.ExamResults.Update(record);
            return _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<ExamResult>> GetAll()
        {
            return await List(FilterByIsDeleted()).ToListAsync();
        }

        public async Task<ExamResult> GetById(int id)
        {
            return await List(FilterById(id)).FirstAsync();
        }
        public async Task<IEnumerable<ExamResult>> GetAllByStudentId(int Id)
        {
            return await _dbContext.ExamResults.Include(x => x.Exams).Where(x => x.StudentId == Id && x.IsDeleted == false).ToListAsync();
        }
        public async Task<int> Edit(ExamResult entity)
        {
            var record = await First(FilterById(entity.Id));
            record.ExamId = entity.ExamId;
            record.Tearm = entity.Tearm;
            record.ObtainMark = entity.ObtainMark;
            record.UpdatedBy = entity.UpdatedBy;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }
        Expression<Func<ExamResult, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
        Expression<Func<ExamResult, bool>> FilterById(int Id)
        {
            return x => x.Id == Id;
        }


    }
}
