using XeroProducts.BL.Dtos.Product;
using XeroProducts.BL.Interfaces;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.BL.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly IProductDALProvider _productDALProvider;
        private readonly IProductOptionProvider _productOptionProvider;

        public ProductProvider(IProductDALProvider productDALProvider,
                                IProductOptionProvider productOptionProvider)
        {
            _productDALProvider = productDALProvider;
            _productOptionProvider = productOptionProvider;
        }

        public async Task<ProductDto?> GetProduct(Guid id)
        {
            var product = await _productDALProvider.GetProduct(id);

            return product != null ? new ProductDto(product) : null;
        }

        public async Task<IList<ProductDto>> GetProducts()
        {
            return await GetProducts(String.Empty);
        }

        public async Task<IList<ProductDto>> GetProducts(string name)
        {
            var products = await _productDALProvider.GetProducts(name);

            return products.Items
                    .Select(p => new ProductDto(p))
                    .ToList();
        }

        public async Task Save(ProductDto product)
        {
            if (product.IsNew)
            {
                await _productDALProvider.CreateProduct(product.ToType());
            }
            else
            {
                await _productDALProvider.UpdateProduct(product.ToType());
            }
        }

        public async Task Delete(Guid productId)
        {
            foreach (var option in (await _productOptionProvider.GetProductOptions(productId)))
            {
                await _productOptionProvider.Delete(option.Id);
            }

            await _productDALProvider.DeleteProduct(productId);
        }
    }
}
