using Identity.API.Models;

namespace Identity.API.Core.IRepository;

public interface IappUserRepository : IGenericRepository<appUsers>
{
    Task<appUsers> getUsersbyEmailId(string emailid);
}