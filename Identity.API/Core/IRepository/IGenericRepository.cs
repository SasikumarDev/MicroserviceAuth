namespace Identity.API.Core.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<bool> Add(T entity);
    bool Delete(T entity);
    Task<IEnumerable<T>> GetAll();
    bool Update(T entity);
    Task<T> GetbyId(Guid Id);
}