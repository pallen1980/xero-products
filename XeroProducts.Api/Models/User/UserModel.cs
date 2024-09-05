using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Required(ErrorMessage = "Please enter a first name")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Please enter a last name")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Please enter an email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Please enter a username")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Please enter a password")]
    public string Password { get; set; }
}