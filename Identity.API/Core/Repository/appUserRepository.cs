using Identity.API.Core.IRepository;
using Identity.API.Data;
using Identity.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Core.Repository;
public class appUserRepository : GenericRepository<appUsers>, IappUserRepository
{
    public appUserRepository(IdentityContext context) : base(context)
    {

    }

    public async Task<appUsers> getUsersbyEmailId(string emailid)
    {
        return await _context.appUsers.FirstOrDefaultAsync(r => r.EmailId == emailid);
    }
}