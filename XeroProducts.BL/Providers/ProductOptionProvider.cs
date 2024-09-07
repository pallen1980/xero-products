using XeroProducts.BL.Dtos.Product;
using XeroProducts.BL.Interfaces;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.BL.Providers
{
    public class ProductOptionProvider : IProductOptionProvider
    {
        private Lazy<IProductOptionDALProvider> _productOptionDALProvider;

        protected IProductOptionDALProvider ProductOptionDALProvider => _productOptionDALProvider.Value;

        public ProductOptionProvider(Lazy<IProductOptionDALProvider> productOptionDALProvider)
        {
            _productOptionDALProvider = productOptionDALProvider;
        }

        public virtual async Task<ProductOptionDto?> GetProductOption(Guid id)
        {
            var product = await ProductOptionDALProvider.GetProductOption(id);

            return product != null ? new ProductOptionDto(product) : null;
        }


        public virtual async Task<IList<ProductOptionDto>> GetProductOptions(Guid productId)
        {
            var products = await ProductOptionDALProvider.GetProductOptions(productId);

            return products.Items.Select(p => new ProductOptionDto(p)).ToList();
        }

        public virtual async Task Save(ProductOptionDto productOptionDto)
        {
            if (productOptionDto.IsNew)
            {
                await ProductOptionDALProvider.CreateProductOption(productOptionDto.ToType());
            }
            else
            {
                await ProductOptionDALProvider.UpdateProductOption(productOptionDto.ToType());
            }
        }

        public virtual async Task Delete(Guid productOptionId)
        {
            await ProductOptionDALProvider.DeleteProductOption(productOptionId);
        }
    }
}
