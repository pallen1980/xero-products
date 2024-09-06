using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Required(ErrorMessage = "Please enter a first name")]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "Please enter a last name")]
    public required string LastName { get; set; }
    
    [Required(ErrorMessage = "Please enter an email")]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Please enter a username")]
    [MinLength(8, ErrorMessage = "Username must be at least 8 characters")]
    [MaxLength(100, ErrorMessage = "Username must be less than 100 characters")]
    public required string Username { get; set; }
    
    [Required(ErrorMessage = "Please enter a password")]
    [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
    public required string Password { get; set; }
}