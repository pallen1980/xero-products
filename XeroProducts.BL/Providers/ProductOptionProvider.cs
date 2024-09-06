using XeroProducts.BL.Dtos.Product;
using XeroProducts.BL.Interfaces;
using XeroProducts.DAL.Interfaces;
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

        public async Task<ProductOptionDto> GetProductOption(Guid id)
        {
            var product = await _productOptionDALProvider.GetProductOption(id);

            return new ProductOptionDto(product);
        }


        public async Task<IList<ProductOptionDto>> GetProductOptions(Guid productId)
        {
            var products = await _productOptionDALProvider.GetProductOptions(productId);

            return products.Items.Select(p => new ProductOptionDto(p)).ToList();
        }

        public async Task Save(ProductOptionDto productOptionDto)
        {
            if (productOptionDto.IsNew)
            {
                await _productOptionDALProvider.CreateProductOption(productOptionDto.ToType());
            }
            else
            {
                await _productOptionDALProvider.UpdateProductOption(productOptionDto.ToType());
            }
        }

        public async Task Delete(Guid productOptionId)
        {
            await _productOptionDALProvider.DeleteProductOption(productOptionId);
        }
    }
}
