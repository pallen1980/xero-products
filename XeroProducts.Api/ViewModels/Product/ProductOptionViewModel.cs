using XeroProducts.BL.Dtos.Product;

public class ProductOptionViewModel
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public ProductOptionViewModel(ProductOptionDto option)
    {
        Id = option.Id;
        ProductId = option.ProductId;
        Name = option.Name;
        Description = option.Description;
    }
}