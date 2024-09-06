using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace XeroProducts.DAL.Sql.Providers
{
    public abstract class BaseSqlProvider
    {
        protected readonly IConfiguration _configuration;

        public BaseSqlProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual SqlConnection NewConnection()
        {
            var connectionString = _configuration.GetConnectionString("Default")?
                .Replace("{DataDirectory}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\App_Data");

            return new SqlConnection(connectionString);
        }
    }
}
