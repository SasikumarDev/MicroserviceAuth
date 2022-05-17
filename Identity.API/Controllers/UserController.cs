using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Identity.API.Core.IConfiguration;
using Identity.API.Dtos;
using Identity.API.Helpers;
using Identity.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUnitofWork _unitofWork;
    private readonly IEncryptPassword _encryptPassword;
    private readonly IMapper _mapper;

    public UserController(IConfiguration configuration,
    IUnitofWork unitofWork, IEncryptPassword encryptPassword, IMapper mapper)
    {
        _configuration = configuration;
        _unitofWork = unitofWork;
        _encryptPassword = encryptPassword;
        _mapper = mapper;
    }

    [HttpPost, AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> Register(appUserVmAdd userVmAdd)
    {
        userVmAdd.Password = _encryptPassword.EncryptPasswordSh(userVmAdd.Password);
        var newUser = _mapper.Map<appUsers>(userVmAdd);
        await _unitofWork.userRepository.Add(newUser);
        await _unitofWork.SaveChangeAsync();
        string access_token = _encryptPassword.generateToken(newUser);
        return new OkObjectResult(new { access_token });
    }
    [HttpPost, AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> Login(LoginVm loginVm)
    {
        loginVm.Password = _encryptPassword.EncryptPasswordSh(loginVm.Password);
        var user = await _unitofWork.userRepository.getUsersbyEmailId(loginVm.Username);
        if (user is null)
        {
            return BadRequest(new { Message = "Invalid Username" });
        }
        if (loginVm.Password != user.Password)
        {
            return BadRequest(new { Message = "Invalid Password" });
        }
        string access_token = _encryptPassword.generateToken(user);
        return new OkObjectResult(new { access_token });
    }

    [Authorize]
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetUserDetails()
    {
        var identityUser = User.Identity as ClaimsIdentity;
        var usId = identityUser.Claims.Cast<Claim>().Where(x => x.Type == "usId").FirstOrDefault()?.Value;
        var currentUser = await _unitofWork.userRepository.GetbyId(Guid.Parse(usId));
        UserTokenDetail detals = new UserTokenDetail()
        {
            EmailID = currentUser.EmailId,
            Name = $"{currentUser.Fname} {currentUser.Mname} {currentUser.Lname}",
            usId = currentUser.usId
        };
        return Ok(detals);
    }
}
