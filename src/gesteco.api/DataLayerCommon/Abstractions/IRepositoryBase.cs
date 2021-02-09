using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace gesteco.api.src.gesteco.WebApi.DataLayer.Abstractions {
  
    public interface IRepositoryBase<T> {
        T Find(long id);
        T Find(long id, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindAll();
        List<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        T Find(Expression<Func<T, bool>> expression);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        List<T> FindByCondition(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        bool Any(T entity);
    }
}
