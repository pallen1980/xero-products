using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.DAL.EntityFramework.Sql.Contexts;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.DAL.EntityFramework.Sql.Providers
{
    public class ProductEntityFrameworkSqlProvider : BaseEntityFrameworkSqlProvider, IProductDALProvider
    {
        public ProductEntityFrameworkSqlProvider(IXeroProductsContext context) : base(context)
        {

        }

        public Task<Guid> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Products> GetProducts(string name = "")
        {
            var products = await Context.Products
                            .Where(p => p.Name.ToLower().Contains(name.Trim().ToLower()))
                            .ToListAsync();

            return new Products(products);
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
