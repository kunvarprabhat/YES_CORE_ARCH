using YES.Domain.Entities;
using YES.Dtos;

namespace YES.Infrastructure.UnitOfWork.Interfaces
{
    public interface IUtilityRepository<T> where T : class
    {
        public Task<APIResponse<T>> Create(T entity);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int Id);
        public Task<int> DeleteRecord(T entity);
        public Task<int> Edit(T entity);
    }
}
