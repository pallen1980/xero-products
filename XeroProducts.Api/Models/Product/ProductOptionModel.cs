using System.ComponentModel.DataAnnotations;

public class ProductOptionModel
{
    [Required(ErrorMessage = "Every option needs a name")]
    [MinLength(3, ErrorMessage = "The name should really be 3 or more characters")]
    [MaxLength(100, ErrorMessage = "The name should really be less than 100 characters or less")]
    public string Name { get; set; }

    [MaxLength(500, ErrorMessage = "Unfortunately you can't have a desription that long. It needs to be 500 characters or less")]
    public string? Description { get; set; }
}