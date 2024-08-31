using XeroProducts.BL.Interfaces;
using XeroProducts.DAL;
using XeroProducts.Types;

namespace XeroProducts.BL.Providers
{
    public class ProductOptionProvider : IProductOptionProvider
    {
        private IProductOptionDALProvider _productOptionDALProvider;

        public ProductOptionProvider(IProductOptionDALProvider productOptionDALProvider)
        {
            _productOptionDALProvider = productOptionDALProvider;
        }

        public async Task<ProductOption> GetProductOption(Guid id)
        {
            return await _productOptionDALProvider.GetProductOption(id);
        }


        public async Task<ProductOptions> GetProductOptions(Guid productId)
        {
            return await _productOptionDALProvider.GetProductOptions(productId);
        }

        public async Task Save(ProductOption productOption)
        {
            if (productOption.IsNew)
            {
                await _productOptionDALProvider.CreateProductOption(productOption);
            }
            else
            {
                await _productOptionDALProvider.UpdateProductOption(productOption);
            }
        }

        public async Task Delete(Guid productOptionId)
        {
            await _productOptionDALProvider.DeleteProductOption(productOptionId);
        }
    }
}
