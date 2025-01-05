using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YES.Infrastructure.UnitOfWork.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        IQueryable<T> List(Expression<Func<T, bool>> expression);
        IQueryable<T> List();
        Task<T> First(Expression<Func<T, bool>> expression);
    }
}
