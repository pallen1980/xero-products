using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.BL.Interfaces
{
    public interface IProductOptionProvider
    {
        Task<ProductOption> GetProductOption(Guid id);
        Task<ProductOptions> GetProductOptions(Guid productId);
        Task Save(ProductOption productOption);
        Task Delete(Guid productOptionId);
    }
}
