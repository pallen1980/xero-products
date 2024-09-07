using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using XeroProducts.DAL.Helpers;

namespace XeroProducts.DAL.Sql.Providers
{
    public abstract class BaseSqlProvider
    {
        private readonly Lazy<IConfiguration> _configuration;
        protected IConfiguration Configuration => _configuration.Value;

        public BaseSqlProvider(Lazy<IConfiguration> configuration)
        {
            _configuration = configuration;
        }

        public virtual SqlConnection NewConnection()
        {
            return new SqlConnection(ConnectionHelper.GetDefaultConnectionString(Configuration));
        }
    }
}
