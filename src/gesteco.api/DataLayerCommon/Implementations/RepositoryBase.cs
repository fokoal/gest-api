using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.DataLayer.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace gesteco.api.src.gesteco.WebApi.DataLayer.Implementations {
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T:class {
        protected GestecoContext _GestecoContext { get; set; }
        protected RepositoryBase(GestecoContext GestecoContext)
        {
            _GestecoContext = GestecoContext;
        }
        public T Create(T entity)
        {
            _GestecoContext.Set<T>().Add(entity);
            _GestecoContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _GestecoContext.Set<T>().Remove(entity);
            _GestecoContext.SaveChanges();
        }

        public IQueryable<T> FindAll()
        {

            return _GestecoContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _GestecoContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            _GestecoContext.Set<T>().Update(entity);
            _GestecoContext.SaveChanges();
        }

        public List<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            var context = _GestecoContext.Set<T>().AsQueryable();
            foreach (var prop in includeProperties)
            {
                context.Include(prop);
            }
            return context.ToList();
        }

        public T Find(long id)
        {
            return _GestecoContext.Set<T>().Find(id);
        }

        public T Find(long id, params Expression<Func<T, object>>[] includeProperties)
        {
            var context = _GestecoContext.Set<T>();
            foreach (var prop in includeProperties)
            {
                context.Include(prop);
            }
            return context.Find(id);
        }

        public List<T> FindByCondition(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            var context = _GestecoContext.Set<T>();
            foreach (var prop in includeProperties)
            {
                context.Include(prop);
            }
            return context.Where(expression).ToList();
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return _GestecoContext.Set<T>().SingleOrDefault(expression);
        }

        public bool Any(T entity)
        {
            return _GestecoContext.Set<T>().Any();
        }
    }
}
