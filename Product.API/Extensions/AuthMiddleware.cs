using Microsoft.Net.Http.Headers;
using Product.API.Services;

namespace Product.API.Extensions;

public class AuthMiddleware
{
    private readonly ILogger<AuthMiddleware> _logger;
    private readonly RequestDelegate _next;
    public AuthMiddleware(ILogger<AuthMiddleware> logger, RequestDelegate requestDelegate)
    {
        _logger = logger;
        _next = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            var _identityService = httpContext.RequestServices.GetRequiredService<IIdentityService>(); // Get Required service
            string token = Convert.ToString(httpContext.Request.Headers[HeaderNames.Authorization]);
            if (!string.IsNullOrEmpty(token))
            {
                var (details, sCode) = await _identityService.getTokenDetails(token);
                if (sCode != 200)
                {
                    httpContext.Response.StatusCode = sCode;
                    return;
                }
            }
            await _next.Invoke(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Middleware error {ex.Message}");
        }
    }
}