using XeroProducts.Types;

namespace XeroProducts.DAL
{
    public interface IProductDALProvider
    {
        /// <summary>
        /// Grab the product matching the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProduct(Guid id);

        /// <summary>
        /// Grab any product that matches the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Products> GetProducts(string name = "");

        /// <summary>
        /// Create a new product and commit it to the data store
        /// </summary>
        /// <param name="product"></param>
        Task<Guid> CreateProduct(Product product);

        /// <summary>
        /// Update an existing product within the data store
        /// </summary>
        /// <param name="product"></param>
        Task UpdateProduct(Product product);

        /// <summary>
        /// Remove an existing product from the data store
        /// </summary>
        /// <param name="id"></param>
        Task DeleteProduct(Guid id);
    }
}
