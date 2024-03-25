
namespace OrderService.Contracts
{
    public interface IGenericRepository<T> where T: class
    {
        T? GetById(long id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string include);
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T FindByCondition(Expression<Func<T, bool>> expression, string includes);
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression, string[] includes = null);
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>>[] expressions, string[] includes = null);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    

}

