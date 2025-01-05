using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;

namespace YES.Infrastructure.Repositories
{
    public class CourseEnquiryRepository : Repository<CourseEnquiry>, ICourseEnquiryRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _appDbContext;
        public CourseEnquiryRepository(DbFactory dbFactory, IUnitOfWork unitOfWork, AppDbContext appDbContext) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
            _appDbContext = appDbContext;
        }
        public async Task<APIResponse<CourseEnquiry>> Create(CourseEnquiry entity)
        {
            APIResponse<CourseEnquiry> response = new APIResponse<CourseEnquiry>(); 
            if (!_appDbContext.CourseEnquiries.Any(x => x.Phone == entity.Phone || x.Email == entity.Email))
            {
                Add(entity);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
            }
            else
            {
                response.Message = "Your set is allreay booked!";
            }

            response.Data = entity;
            return await Task.FromResult(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CourseEnquiry>> GetAll()
        {
            return await List(FilterByIsDeleted()).ToListAsync();
        }
        Expression<Func<CourseEnquiry, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
    }
}
