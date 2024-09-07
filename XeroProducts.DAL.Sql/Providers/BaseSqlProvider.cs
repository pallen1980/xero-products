using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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
            var connectionString = Configuration.GetConnectionString("Default")?
                .Replace("{DataDirectory}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\App_Data");

            return new SqlConnection(connectionString);
        }
    }
}
