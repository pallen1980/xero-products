using XeroProducts.BL.Dtos.Product;

public class ProductsViewModel
{
    public List<ProductViewModel> Items { get; set; }

    public ProductsViewModel(IList<ProductDto> products)
    {
        Items = new List<ProductViewModel>(products.Select(p => new ProductViewModel(p)).ToList());
    }
}