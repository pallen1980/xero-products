using XeroProducts.Types;

public class ProductsViewModel
{
    public List<ProductViewModel> Items { get; set; }

    public ProductsViewModel(Products products)
    {
        Items = new List<ProductViewModel>(products.Items.Select(p => new ProductViewModel(p)).ToList());
    }
}