using Identity.API.Core.IRepository;
using Identity.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Core.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly IdentityContext _context;
    public GenericRepository(IdentityContext context)
    {
        _context = context;
    }

    public virtual async Task<bool> Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return true;
    }

    public virtual bool Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return true;
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetbyId(Guid Id)
    {
        return await _context.Set<T>().FindAsync(Id);
    }

    public virtual bool Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return true;
    }
}