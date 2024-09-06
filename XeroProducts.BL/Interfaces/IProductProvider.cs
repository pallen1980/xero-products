using XeroProducts.BL.Dtos.Product;

namespace XeroProducts.BL.Interfaces
{
    public interface IProductProvider
    {
        Task<ProductDto?> GetProduct(Guid id);
        Task<IList<ProductDto>> GetProducts();
        Task<IList<ProductDto>> GetProducts(string name);
        Task Save(ProductDto product);
        Task Delete(Guid productId);
    }
}
