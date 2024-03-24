using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace OrderService.Repository
{
    
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext _applicationContext { get; set; }
        public GenericRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public T? GetById(long id)
        {
            return _applicationContext.Set<T>()
                                      .Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _applicationContext.Set<T>().ToList();
            //return _applicationContext.Set<T>().AsNoTracking();
        }

        public IEnumerable<T> GetAll(string include)
        {
            IQueryable<T> query = _applicationContext.Set<T>();

            if (!string.IsNullOrEmpty(include))
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _applicationContext.Set<T>().Where(expression);
            //return _applicationContext.Set<T>().Where(expression).AsNoTracking();
        }

        public T FindByCondition(Expression<Func<T, bool>> expression, string include)
        {
            IQueryable<T> query = _applicationContext.Set<T>();

            if (!string.IsNullOrWhiteSpace(include))
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(expression);
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            IQueryable<T> query = _applicationContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(expression).ToList();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>>[] expressions, string[] includes = null)
        {
            IQueryable<T> query = _applicationContext.Set<T>();

            foreach (var expression in expressions)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public void Create(T entity)
        {
            _applicationContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _applicationContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _applicationContext.Set<T>().Remove(entity);
        }

    }

   
}
