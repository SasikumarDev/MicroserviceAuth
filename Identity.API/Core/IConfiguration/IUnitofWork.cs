using Identity.API.Core.IRepository;

namespace Identity.API.Core.IConfiguration;

public interface IUnitofWork
{
    IappUserRepository userRepository { get; }
    Task SaveChangeAsync();
}