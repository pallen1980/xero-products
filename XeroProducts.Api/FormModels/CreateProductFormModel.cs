using System.ComponentModel.DataAnnotations;
using XeroProducts.Types;

public class CreateProductFormModel : ProductModel
{
    public Product ToType()
    {
        return new Product()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Description = Description ?? "",
            Price = Price,
            DeliveryPrice = DeliveryPrice,
        };
    }
}