using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.DAL.Interfaces
{
    public interface IProductOptionDALProvider
    {
        /// <summary>
        /// Grab the product option that matches the given ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<ProductOption?> GetProductOption(Guid productOptionId);

        /// <summary>
        /// Grab the all product options matching the given product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<ProductOptions> GetProductOptions(Guid productId);

        /// <summary>
        /// Create a ProductOption and commit it to the data store
        /// </summary>
        /// <param name="productOption"></param>
        /// <returns></returns>
        Task<Guid> CreateProductOption(ProductOption productOption);

        /// <summary>
        /// Update an existing ProductOption in the data store
        /// </summary>
        /// <param name="productOption"></param>
        /// <returns></returns>
        Task UpdateProductOption(ProductOption productOption);

        /// <summary>
        /// Remove an existing ProductOption in the data store
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        Task DeleteProductOption(Guid productOptionId);
    }
}
