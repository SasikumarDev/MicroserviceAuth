using System.Text.Json;
using Product.API.Dtos;
using Product.API.Extensions;

namespace Product.API.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _client;
    private readonly ILogger<IdentityService> _logger;
    public IdentityService(HttpClient client, ILogger<IdentityService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<(UserTokenDetail, int)> getTokenDetails(string accesToken)
    {
        try
        {
            _logger.LogInformation("Make Http call to get user Details");
            _client.DefaultRequestHeaders.Add("Authorization", accesToken);
            var response = await _client.GetAsync("/User/GetUserDetails");
            _logger.LogInformation($"Status Code from service : {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.ReadContentAs<UserTokenDetail>();
                return (data, (int)response.StatusCode);
            }
            return (null, (int)response.StatusCode);
        }
        catch (HttpRequestException rex)
        {
            _logger.LogError($"error Occured in Identity Server ${rex.Message}");
            int statuscode = (int)rex.StatusCode;
            return (null, statuscode);
        }
        catch (Exception ex)
        {
            _logger.LogError($"error Occured in Application ${ex.Message}");
            return (null, 500);
        }
    }
}