using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.DAL.EntityFramework.Sql.Contexts
{
    public interface IXeroProductsContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductOption> ProductOptions { get; set; }
        DbSet<User> Users { get; set; }
    }
}
