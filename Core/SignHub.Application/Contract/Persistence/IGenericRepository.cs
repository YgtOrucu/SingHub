using System.Linq.Expressions;

namespace SignHub.Application.Contract.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetListAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
    IQueryable GetByQueries();
    Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
}
