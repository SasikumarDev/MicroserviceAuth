using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Helpers;

public class EncryptPassword : IEncryptPassword
{
    private readonly IConfiguration _configuration;
    public EncryptPassword(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string EncryptPasswordSh(string Password)
    {
        StringBuilder hashPass = new StringBuilder();
        using SHA256 sHA = SHA256.Create();
        byte[] bdata = sHA.ComputeHash(Encoding.UTF8.GetBytes(Password));
        foreach (byte b in bdata)
        {
            hashPass.Append(b.ToString("X2"));
        }
        return hashPass.ToString();
    }
    public string generateToken(appUsers appUsers)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var Claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub,appUsers.usId.ToString()),
            new Claim("usId",appUsers.usId.ToString()),
            new Claim("Role",appUsers.Role),
            new Claim("EmailId",appUsers.EmailId)
        };
        var token = new JwtSecurityToken(_configuration["JwtConfig:Issuer"].ToString(),
        _configuration["JwtConfig:Issuer"].ToString(), Claims, DateTime.Now, DateTime.Now.AddHours(1), cred);
        var access_token = new JwtSecurityTokenHandler().WriteToken(token);
        return access_token;
    }
}