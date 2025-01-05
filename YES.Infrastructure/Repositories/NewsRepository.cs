using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YES.Domain.Entities;
using YES.Dtos;
using YES.Infrastructure.IRepositories;
using YES.Infrastructure.UnitOfWork.Interfaces;
using YES.Infrastructure.UnitOfWork.Repository;

namespace YES.Infrastructure.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsRepository(DbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<News>> Create(News entity)
        {
            APIResponse<News> response = new APIResponse<News>();
            Add(entity);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);
            response.Data = entity;
            return await Task.FromResult(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await List(FilterByIsDeleted()).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<News> GetById(int Id)
        {
            return await First(FilterById(Id));
        }

        public async Task<int> DeleteRecord(News entity)
        {
            var record = await First(FilterById(entity.Id));
            record.IsDeleted = !record.IsDeleted;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task<int> Edit(News entity)
        {
            var record = await First(FilterById(entity.Id));
            record.Title = entity.Title;
            record.Details = entity.Details;
            record.UpdatedBy = entity.UpdatedBy;
            Update(record);
            return await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }
        Expression<Func<News, bool>> FilterByIsDeleted()
        {
            return x => x.IsDeleted == false;
        }
        Expression<Func<News, bool>> FilterById(int Id)
        {
            return x => x.Id == Id;
        }
    }
}
