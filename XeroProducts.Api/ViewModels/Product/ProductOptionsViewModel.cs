using XeroProducts.BL.Dtos.Product;

public class ProductOptionsViewModel
{
    public List<ProductOptionViewModel> Items { get; set; }

    public ProductOptionsViewModel(IList<ProductOptionDto> options)
    {
        Items = new List<ProductOptionViewModel>(options.Select(p => new ProductOptionViewModel(p)).ToList());
    }
}