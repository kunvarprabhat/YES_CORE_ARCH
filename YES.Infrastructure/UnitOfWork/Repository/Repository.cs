using Microsoft.EntityFrameworkCore;
using YES.Infrastructure.UnitOfWork.Interfaces;
using System.Linq.Expressions;
using static YES.Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YES.Infrastructure.UnitOfWork.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly DbFactory _dbFactory;
        public DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<T>());
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Add(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).CreatedDate = DateTime.Now;
            }
            if (typeof(IBaseAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IBaseAuditEntity)entity).CreatedDate = DateTime.Now;
            }
            DbSet.Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                foreach (var entity in entities)
                {
                    ((IAuditEntity)entity).CreatedDate = DateTime.Now;
                }
            }
            DbSet.AddRange(entities);
        }
        public void Update(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).UpdatedDate = DateTime.Now;
            }
            DbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                foreach (var entity in entities)
                {
                    ((IAuditEntity)entity).UpdatedDate = DateTime.Now;
                }
            }
            DbSet.UpdateRange(entities);
        }
        public void Attach(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).UpdatedDate = DateTime.Now;
            }
            DbSet.Attach(entity);
        }
        public void AttachRange(IEnumerable<T> entities)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                foreach (var entity in entities)
                {
                    ((IAuditEntity)entity).UpdatedDate = DateTime.Now;
                }
            }
            DbSet.AttachRange(entities);
        }
        public void Delete(T entity)
        {
            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(T)))
            {
                ((IDeleteEntity)entity).IsDeleted = true;
                DbSet.Update(entity);
            }
            else
                DbSet.Remove(entity);
        }
        public Task<T> First(Expression<Func<T, bool>> expression)
        {
            return DbSet.FirstAsync(expression);
        }
        public IQueryable<T> List(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }
        public IQueryable<T> List()
        {
            return DbSet;
        }


    }
}
