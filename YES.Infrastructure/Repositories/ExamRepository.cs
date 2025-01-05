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
    public class ExamRepository : Repository<Exam>, IExamRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        public ExamRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext dbContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }


        public async Task<APIResponse<Exam>> Create(Exam entity)
        {
            APIResponse<Exam> response = new APIResponse<Exam>();
            Add(entity);
            var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
            response.Data = entity;
            return await Task.FromResult(response).ConfigureAwait(false);

        }
        public async Task<APIResponse<Exam>> Create(List<Exam> entity)
        {
            APIResponse<Exam> apiResponse = new APIResponse<Exam>();
            try
            {
                AddRange(entity);
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

        public async Task<IEnumerable<Exam>> GetAll()
        {
            return await _dbContext.Exams.Include(x => x.Course).Where(x=>!x.IsDeleted).ToListAsync();
        }

        public async Task<Exam> GetById(int id)
        {
            return await First(FilterById(id));
        }
        public async Task<IEnumerable<Exam>> GetAllByCourseId(int courseId)
        {
            return await _dbContext.Exams.Include(x => x.Course).Where(x => x.CourseId == courseId && !x.IsDeleted).ToListAsync();
        }
        public async Task<int> DeleteRecord(Exam entity)
        {

            var record = await First(FilterById(entity.Id));
            record.IsDeleted = !record.IsDeleted;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);


        }

        public async Task<int> Edit(Exam entity)
        {
            var record = await First(FilterById(entity.Id));
            record.UpdatedBy = entity.UpdatedBy;
            record.CourseId = entity.CourseId;
            record.Modules = entity.Modules;
            record.ExamName = entity.ExamName;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }
        Expression<Func<Exam, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
        Expression<Func<Exam, bool>> FilterById(int Id)
        {
            return x => x.Id == Id;
        }


    }
}
