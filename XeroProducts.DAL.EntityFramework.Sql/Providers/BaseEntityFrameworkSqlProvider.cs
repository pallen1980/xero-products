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
        protected IXeroProductsContext Context;

        public BaseEntityFrameworkSqlProvider(IXeroProductsContext context)
        {
            Context = context;
        }
    }
}
