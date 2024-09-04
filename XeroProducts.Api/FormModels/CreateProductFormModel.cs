using System.ComponentModel.DataAnnotations;
using XeroProducts.Types;

public class CreateProductFormModel : ProductModel
{
    /// <summary>
    /// Convert this FormModel to a Type
    /// </summary>
    /// <returns></returns>
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