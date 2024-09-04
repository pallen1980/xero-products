using XeroProducts.Types;

public class CreateProductOptionFormModel : ProductOptionModel
{
    
    /// <summary>
    /// Convert this FormModel to a Type
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public ProductOption ToType(Guid productId)
    {
        return new ProductOption()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Name = Name,
            Description = Description ?? ""
        };
    }
}