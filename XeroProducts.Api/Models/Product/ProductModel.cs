using System.ComponentModel.DataAnnotations;

public class ProductModel
{
    [Required(ErrorMessage = "Every product needs a name")]
    [MinLength(3, ErrorMessage = "The name should really be 3 or more characters")]
    [MaxLength(100, ErrorMessage = "The name should really be less than 100 characters or less")]
    public required string Name { get; set; }

    [MaxLength(500, ErrorMessage = "Unfortunately you can't have a desription that long. It needs to be 500 characters or less")]
    public string? Description { get; set; }

    [Range(100, double.MaxValue, ErrorMessage = "We're not giving them away! The price should really be higher than 100")]
    public decimal Price { get; set; }

    [Range(0, 100, ErrorMessage = "That's quite a lot for delivery! The delivery price shouldn't be more than 100")]
    public decimal DeliveryPrice { get; set; }
}