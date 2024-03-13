using System.Linq.Expressions;

namespace OrderService.Contracts
{
    public interface IGenericRepository<T> where T: class
    {
        T? GetById(long id);
        IEnumerable<T> GetAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    

}

