using XeroProducts.Types;

public class ProductOptionsViewModel
{
    public List<ProductOptionViewModel> Items { get; set; }

    public ProductOptionsViewModel(ProductOptions options)
    {
        Items = new List<ProductOptionViewModel>(options.Items.Select(p => new ProductOptionViewModel(p)).ToList());
    }
}