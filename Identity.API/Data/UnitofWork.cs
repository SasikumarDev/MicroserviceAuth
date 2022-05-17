using Identity.API.Core.IConfiguration;
using Identity.API.Core.IRepository;
using Identity.API.Core.Repository;

namespace Identity.API.Data;

public class UnitofWork : IUnitofWork, IDisposable
{
    private readonly IdentityContext _context;
    public UnitofWork(IdentityContext context)
    {
        _context = context;
        userRepository = new appUserRepository(_context);
    }

    public IappUserRepository userRepository { get; private set; }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
}