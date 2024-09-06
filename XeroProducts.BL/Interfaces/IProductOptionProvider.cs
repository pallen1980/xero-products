using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Dtos.Product;
using XeroProducts.Types;

namespace XeroProducts.BL.Interfaces
{
    public interface IProductOptionProvider
    {
        Task<ProductOptionDto?> GetProductOption(Guid id);
        Task<IList<ProductOptionDto>> GetProductOptions(Guid productId);
        Task Save(ProductOptionDto productOptionDto);
        Task Delete(Guid productOptionId);
    }
}
