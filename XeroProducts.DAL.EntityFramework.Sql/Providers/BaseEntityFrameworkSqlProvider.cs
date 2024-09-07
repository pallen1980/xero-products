using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.DAL.EntityFramework.Sql.Contexts;

namespace XeroProducts.DAL.EntityFramework.Sql.Providers
{
    public abstract class BaseEntityFrameworkSqlProvider
    {
        private readonly Lazy<IXeroProductsContext> _context;
        protected IXeroProductsContext Context => _context.Value;

        public BaseEntityFrameworkSqlProvider(Lazy<IXeroProductsContext> context)
        {
            _context = context;
        }
    }
}
