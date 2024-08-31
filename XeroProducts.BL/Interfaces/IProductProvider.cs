using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.BL.Interfaces
{
    public interface IProductProvider
    {
        Task<Product> GetProduct(Guid id);
        Task<Products> GetProducts();
        Task<Products> GetProducts(string name);
        Task Save(Product product);
        Task Delete(Guid productId);
    }
}
