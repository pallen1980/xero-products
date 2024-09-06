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
    public class ProductOptionEntityFrameworkSqlProvider : BaseEntityFrameworkSqlProvider, IProductOptionDALProvider
    {
        public ProductOptionEntityFrameworkSqlProvider(IXeroProductsContext context) : base(context)
        {

        }

        public Task<Guid> CreateProductOption(ProductOption productOption)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductOption(Guid productOptionId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductOption> GetProductOption(Guid productOptionId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductOptions> GetProductOptions(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductOption(ProductOption productOption)
        {
            throw new NotImplementedException();
        }
    }
}
