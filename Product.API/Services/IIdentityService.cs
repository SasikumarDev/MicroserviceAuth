using Product.API.Dtos;

namespace Product.API.Services;

public interface IIdentityService
{
    Task<(UserTokenDetail,int)> getTokenDetails(string accesToken);
}