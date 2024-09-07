using Microsoft.EntityFrameworkCore;
using XeroProducts.DAL.EntityFramework.Sql.Contexts;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.DAL.EntityFramework.Sql.Providers
{
    public class ProductOptionEntityFrameworkSqlProvider : BaseEntityFrameworkSqlProvider, IProductOptionDALProvider
    {
        public ProductOptionEntityFrameworkSqlProvider(Lazy<IXeroProductsContext> context) : base(context)
        {

        }

        /// <summary>
        /// Return the product option matching the given ID
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        public virtual async Task<ProductOption?> GetProductOption(Guid productOptionId)
        {
            return await Context.ProductOptions.SingleOrDefaultAsync(po => po.Id == productOptionId);
        }

        /// <summary>
        /// Return all product options for the given product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public virtual async Task<ProductOptions> GetProductOptions(Guid productId)
        {
            var options = await Context.ProductOptions
                            .Where(po => po.ProductId == productId)
                            .ToListAsync();

            return new ProductOptions(options);
        }

        /// <summary>
        /// Adds/Attaches the given product option to the context and saves the context
        /// </summary>
        /// <param name="productOption"></param>
        /// <returns></returns>
        public virtual async Task<Guid> CreateProductOption(ProductOption productOption)
        {
            await Context.ProductOptions.AddAsync(productOption);

            await Context.SaveChangesAsync();

            return productOption.Id;
        }

        /// <summary>
        /// Find the product option in the context, updates the change-able properties and saves the context
        /// </summary>
        /// <param name="productOption"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual async Task UpdateProductOption(ProductOption productOption)
        {
            var dbProductOption = await Context.ProductOptions.SingleOrDefaultAsync(po => po.Id == productOption.Id);

            if (dbProductOption == null)
            {
                throw new KeyNotFoundException($"Update Failed: No Product Option found matching id: {productOption.Id}");
            }

            dbProductOption.Name = productOption.Name;
            dbProductOption.Description = productOption.Description;

            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Find the product option in the context, remove it, then save the context
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual async Task DeleteProductOption(Guid productOptionId)
        {
            var productOption = await Context.ProductOptions.SingleOrDefaultAsync(po => po.Id == productOptionId);

            if (productOption == null)
            {
                throw new KeyNotFoundException($"Delete Failed: No Product Option found matching id: {productOptionId}");
            }

            Context.ProductOptions.Remove(productOption);

            await Context.SaveChangesAsync();
        }
    }
}
