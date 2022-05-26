using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Product.API.Dtos;
using Product.API.Helper;
using Product.API.Repositories;
using Product.API.Services;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<ProductController> _logger;
    private readonly IUnitofWork _unitofWork;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;
    private readonly IFileHelper _fileHelper;
    public ProductController(ILogger<ProductController> logger, IIdentityService identityService,
     IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment, IMapper mapper, IFileHelper fileHelper)
    {
        _identityService = identityService;
        _logger = logger;
        _unitofWork = unitofWork;
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
        _fileHelper = fileHelper;
    }

    [Authorize(Policy = "Admin")]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetUserDetails()
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

    [HttpGet, AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> GetProducts()
    {
        string urlpath = Request.Scheme + "://" + Request.Host.Value + Url.Content("/PartImages");
        var data = await _unitofWork.productRepository.GetAll();
        return Ok(data);
    }

    [HttpGet, AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> getProductsDisplay()
    {
        string urlpath = Request.Scheme + "://" + Request.Host.Value + Url.Content("/PartImages/");
        var data = await _unitofWork.productRepository.getProductsDisplay(urlpath);
        return Ok(data);
    }

    [Authorize(Policy = "Admin")]
    [HttpPost, DisableRequestSizeLimit]
    [Route("[action]")]
    public async Task<IActionResult> SaveProduct([FromForm] ProductAddVm productAdd)
    {
        string token = Request.Headers[HeaderNames.Authorization].ToString();
        var (userDetails, code) = await _identityService.getTokenDetails(token);
        if (code != 200)
        {
            return StatusCode(code);
        }
        string filepath = _hostEnvironment.WebRootPath + "/PartImages/";
        var product = _mapper.Map<Models.Product>(productAdd);
        product.Images = new List<string>();
        foreach (var file in productAdd.Images)
        {
            string name = await _fileHelper.UploadFile(filepath, file);
            product.Images.Add(name);
        }
        product.Createdby = userDetails.usId.ToString();
        await _unitofWork.productRepository.CreateProduct(product);
        return Ok(product);
    }
}
