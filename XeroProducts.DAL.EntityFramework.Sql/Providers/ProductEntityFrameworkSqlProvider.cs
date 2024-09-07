using Microsoft.EntityFrameworkCore;
using XeroProducts.DAL.EntityFramework.Sql.Contexts;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.DAL.EntityFramework.Sql.Providers
{
    public class ProductEntityFrameworkSqlProvider : BaseEntityFrameworkSqlProvider, IProductDALProvider
    {
        public ProductEntityFrameworkSqlProvider(Lazy<IXeroProductsContext> context) : base(context)
        {

        }

        /// <summary>
        /// Return the product that matches the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<Product?> GetProduct(Guid id)
        {
            return await Context.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Returns all products matching the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual async Task<Products> GetProducts(string name = "")
        {
            var products = await Context.Products
                            .Where(p => p.Name.ToLower().Contains(name.Trim().ToLower()))
                            .ToListAsync();

            return new Products(products);
        }

        /// <summary>
        /// Adds/Attaches the product to the context, and saves the context
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public virtual async Task<Guid> CreateProduct(Product product)
        {
            await Context.Products.AddAsync(product);

            await Context.SaveChangesAsync();
            
            return product.Id;
        }

        /// <summary>
        /// Find the product matching the ID in the given product, updates the changeable properties, and saves the context
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual async Task UpdateProduct(Product product)
        {
            var dbProduct = await Context.Products.SingleOrDefaultAsync(p => p.Id == product.Id);

            if (dbProduct == null)
            {
                throw new KeyNotFoundException($"Update Failed: No Product found matching id: {product.Id}");
            }

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.Price = product.Price;
            dbProduct.DeliveryPrice = product.DeliveryPrice;

            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Find the product matching the given ID, removes it from the context, and saves the context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual async Task DeleteProduct(Guid id)
        {
            var product = await Context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Delete Failed: No Product found matching id: {id}");
            }

            Context.Products.Remove(product);

            await Context.SaveChangesAsync();
        }
    }
}
