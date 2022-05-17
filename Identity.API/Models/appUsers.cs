namespace Identity.API.Models;

public class appUsers
{
    public Guid usId { get; set; }
    public string Fname { get; set; }
    public string Mname { get; set; }
    public string Lname { get; set; }
    public string EmailId { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime Dob { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}