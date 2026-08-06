using Microsoft.EntityFrameworkCore;
using SignHub.Application.Contract.Persistence;
using SignHub.Persistence.Context;
using System.Linq.Expressions;

namespace SignHub.Persistence.Concreate;

public class GenericRepository<T>(SignHubContext context) : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _dbset = context.Set<T>();
    
    async Task<List<T>> IGenericRepository<T>.GetListAsync()
    {
        return await _dbset.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public async Task CreateAsync(T entity)
    {
        await _dbset.AddAsync(entity);
    }
    public void Update(T entity)
    {
        _dbset.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbset.Remove(entity);
    }

    public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbset.AsNoTracking().FirstOrDefaultAsync(filter);
    }

    public IQueryable GetByQueries()
    {
        return _dbset;
    }
}
