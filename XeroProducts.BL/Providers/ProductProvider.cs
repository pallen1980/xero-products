using XeroProducts.BL.Dtos.Product;
using XeroProducts.BL.Interfaces;
using XeroProducts.DAL.Interfaces;

namespace XeroProducts.BL.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly Lazy<IProductDALProvider> _productDALProvider;
        private readonly Lazy<IProductOptionProvider> _productOptionProvider;

        protected IProductDALProvider ProductDALProvider => _productDALProvider.Value;
        protected IProductOptionProvider ProductOptionProvider => _productOptionProvider.Value;

        public ProductProvider(Lazy<IProductDALProvider> productDALProvider,
                               Lazy<IProductOptionProvider> productOptionProvider)
        {
            _productDALProvider = productDALProvider;
            _productOptionProvider = productOptionProvider;
        }

        public virtual async Task<ProductDto?> GetProduct(Guid id)
        {
            var product = await ProductDALProvider.GetProduct(id);

            return product != null ? new ProductDto(product) : null;
        }

        public virtual async Task<IList<ProductDto>> GetProducts()
        {
            return await GetProducts(String.Empty);
        }

        public virtual async Task<IList<ProductDto>> GetProducts(string name)
        {
            var products = await ProductDALProvider.GetProducts(name);

            return products.Items
                    .Select(p => new ProductDto(p))
                    .ToList();
        }

        public virtual async Task Save(ProductDto product)
        {
            if (product.IsNew)
            {
                await ProductDALProvider.CreateProduct(product.ToType());
            }
            else
            {
                await ProductDALProvider.UpdateProduct(product.ToType());
            }
        }

        public virtual async Task Delete(Guid productId)
        {
            foreach (var option in (await ProductOptionProvider.GetProductOptions(productId)))
            {
                await ProductOptionProvider.Delete(option.Id);
            }

            await ProductDALProvider.DeleteProduct(productId);
        }
    }
}
