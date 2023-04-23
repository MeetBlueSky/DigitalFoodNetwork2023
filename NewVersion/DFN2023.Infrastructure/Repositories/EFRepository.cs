using DFN2023.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DFN2023.Infrastructure.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public EFRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public IQueryable<T> Entities => _dbSet;
        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            if (entity.GetType().GetProperty("IsDelete") != null)
            {
                T _entity = entity;

                _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                this.Update(_entity);
            }
            else
            {
                EntityEntry dbEntityEntry = _dbContext.Entry(entity);

                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _dbSet.Attach(entity);
                    _dbSet.Remove(entity);
                }
            }
        }
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                if (entity.GetType().GetProperty("IsDelete") != null)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }
        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public IList<T> GetList()
        {
            return _dbContext.Set<T>().ToList();
        }
        public IList<T> GetList(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).ToList();
        }
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            //IIncludableQueryable<T, object> query = null;
            //if (includes.Length > 0)
            //{
            //    query = _dbSet.Include(includes[0]);
            //}
            //for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            //{
            //    query = query.Include(includes[queryIndex]);
            //}
            //return query == null ? _dbSet : (IQueryable<T>)query;
            IIncludableQueryable<T, object> query = null;
            if (includes.Length > 0)
            {
                query = _dbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }
            return query == null ? _dbSet : (IQueryable<T>)query;


        }
        public T Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public IQueryable<T> FromSql(string sql)
        {
            return _dbSet.FromSqlRaw<T>(sql);
        }
    }
}
