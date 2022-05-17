using System.ComponentModel.DataAnnotations;

namespace Identity.API.Dtos;

public class LoginVm
{
    [Required(ErrorMessage = "Username is Required")]
    [Display(Name = "Username")]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}