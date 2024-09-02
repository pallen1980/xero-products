using XeroProducts.BL.Interfaces;
using XeroProducts.DAL;
using XeroProducts.Types;

namespace XeroProducts.BL.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly IProductDALProvider _productDALProvider;
        private readonly IProductOptionProvider _productOptionProvider;

        public ProductProvider( IProductDALProvider productDALProvider,
                                IProductOptionProvider productOptionProvider)
        {
            _productDALProvider = productDALProvider;
            _productOptionProvider = productOptionProvider;
        }

        public async Task<Product> GetProduct(Guid id)
        {
            return await _productDALProvider.GetProduct(id);
        }

        public async Task<Products> GetProducts()
        {
            return await GetProducts(null);
        }

        public async Task<Products> GetProducts(string name)
        {
            return await _productDALProvider.GetProducts(name);
        }

        public async Task Save(Product product)
        {
            if (product.IsNew)
            {
                await _productDALProvider.CreateProduct(product);
            }
            else
            {
                await _productDALProvider.UpdateProduct(product);
            }
        }

        public async Task Delete(Guid productId)
        {
            foreach (var option in (await _productOptionProvider.GetProductOptions(productId)).Items)
            {
                await _productOptionProvider.Delete(option.Id);
            }

            await _productDALProvider.DeleteProduct(productId);
        }
    }
}
