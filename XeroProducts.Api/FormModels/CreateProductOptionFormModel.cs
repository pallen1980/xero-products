using XeroProducts.Types;

public class CreateProductOptionFormModel : ProductOptionModel
{
    
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