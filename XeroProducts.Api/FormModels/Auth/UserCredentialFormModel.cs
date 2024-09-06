using System.ComponentModel.DataAnnotations;

public record UserCredentialFormModel
{
    [Required(ErrorMessage = "You must supply a username")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "You must supply a password")]
    public required string Password { get; set; }
}