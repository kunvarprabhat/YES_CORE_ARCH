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
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _dbContext;
        public CourseRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext appContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContext = appContext;
        }
        public async Task<APIResponse<Course>> Create(IEnumerable<Course> course)
        {
            var apiResponse = new APIResponse<Course>();
            try
            {
                AddRange(course);
                var result = await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return await Task.FromResult(apiResponse).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Error = new APIException(ex.Message, ex.InnerException);
                apiResponse.Message = CommonResource.AddFailed;
                apiResponse.Status = HttpStatusCode.InternalServerError;
                return apiResponse;

            }
        }
        public async Task<APIResponse<Course>> Create(Course entity)
        {
            var apiResponse = new APIResponse<Course>();
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
                return apiResponse;

            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var result = await _dbContext.Courses.Where(x => !x.IsDeleted).ToArrayAsync();
            return result;
        }
        public async Task<Course> GetCourseById(int Id)
        {
            var result = await First(FilterById(Id));
            return result;
        }
        public async Task<Course> GetById(int Id)
        {
            var result = await _dbContext.Courses.Where(x => !x.IsDeleted).FirstAsync(x => x.Id == Id);
            return result;
        }

        public async Task<int> DeleteRecord(Course entity)
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

        public async Task<int> Edit(Course entity)
        {
            var record = await First(FilterById(entity.Id));
            record.UpdatedBy = entity.UpdatedBy;
            record.CourseShortName = entity.CourseShortName;
            record.CourseFullName = entity.CourseFullName;
            record.Duration = entity.Duration;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }
        public async Task<Course> GetCourseDetailsAsync(int Id)
        {
            return await _dbContext.Courses.Include(x => x.BatchDetails).Include(x => x.SyllabusDetails).FirstOrDefaultAsync(x => x.Id == Id);
        }
        Expression<Func<Course, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }

        Expression<Func<Course, bool>> FilterById(int CId)
        {
            return x => x.Id == CId;
        }


    }
}
