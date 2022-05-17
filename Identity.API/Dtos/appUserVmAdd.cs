using System.ComponentModel.DataAnnotations;

namespace Identity.API.Dtos;

public class appUserVmAdd
{
    [Required(ErrorMessage = "First Name is Required")]
    [Display(Name = "First Name")]
    public string Fname { get; set; }
    public string Mname { get; set; }
    
    [Required(ErrorMessage = "Last Name is Required")]
    [Display(Name = "Last Name")]
    public string Lname { get; set; }

    [Required(ErrorMessage = "Email ID is Required")]
    [Display(Name = "Email ID")]
    [DataType(DataType.EmailAddress)]
    public string EmailId { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [MinLength(5)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Role is Required")]
    [Display(Name = "Role")]
    public string Role { get; set; }

    [Required(ErrorMessage = "Dob is Required")]
    [Display(Name = "Dob")]
    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }
}