using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DFN2023.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }
        T GetById(object id);
        IList<T> GetList();
        IList<T> GetList(Expression<Func<T, bool>> expression);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);

        IQueryable<T> FromSql(string sql);
    }
}
