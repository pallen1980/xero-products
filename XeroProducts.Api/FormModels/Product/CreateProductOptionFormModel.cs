using XeroProducts.BL.Dtos.Product;

public class CreateProductOptionFormModel : ProductOptionModel
{
    
    /// <summary>
    /// Convert this FormModel to a Type
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public ProductOptionDto ToDto(Guid productId)
    {
        return new ProductOptionDto()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Name = Name,
            Description = Description ?? ""
        };
    }
}