using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Product.API.Services;

namespace Product.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, IIdentityService identityService)
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
