using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Product.API.Services;

namespace Product.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger, IIdentityService identityService)
    {
        _identityService = identityService;
        _logger = logger;
    }

    [Authorize(Policy = "Admin")]
    [HttpGet(Name = "GetUserDetails")]
    public async Task<IActionResult> Get()
    {
        string token = Request.Headers[HeaderNames.Authorization].ToString();
        _logger.LogInformation($"access_token : {token}");
        var (userDetails, code) = await _identityService.getTokenDetails(token);
        if (code != 200)
        {
            return StatusCode(code);
        }
        return Ok(userDetails);
    }
}
