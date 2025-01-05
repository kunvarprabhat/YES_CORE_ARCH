using YES.Dtos;

namespace YES.Services.Interface
{
    public interface IUtilityService<T> where T : class
    {
        public Task<APIResponse<T>> CreateAsync(T entity);
        public Task<APIResponse<IEnumerable<T>>> GetAllAsync();
        public Task<APIResponse<T>> GetByIdAsync(int id);
        public Task<APIResponse> DeleteAsync(T entity);
        public Task<APIResponse<T>> EditAsync(T entity);
    }
}
