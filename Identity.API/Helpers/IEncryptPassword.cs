using Identity.API.Models;

namespace Identity.API.Helpers;

public interface IEncryptPassword
{
    string EncryptPasswordSh(string Password);
    string generateToken(appUsers appUsers);
}