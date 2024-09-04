using System.ComponentModel.DataAnnotations;

public record UserCredential
{
    [Required(ErrorMessage = "You must supply a username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "You must supply a password")]
    public string Password { get; set; }
}