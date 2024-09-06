using XeroProducts.BL.Dtos.Product;

public class CreateProductFormModel : ProductModel
{
    /// <summary>
    /// Convert this FormModel to a Type
    /// </summary>
    /// <returns></returns>
    public ProductDto ToDto()
    {
        return new ProductDto()
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Description = Description ?? "",
            Price = Price,
            DeliveryPrice = DeliveryPrice,
        };
    }
}