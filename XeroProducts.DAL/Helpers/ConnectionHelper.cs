using Microsoft.Extensions.Configuration;

namespace XeroProducts.DAL.Helpers
{
    public static class ConnectionHelper
    {
        /// <summary>
        /// Returns the connection string matching the given name
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static string? GetConnectionString(IConfiguration configuration, string connectionName)
        {
            //Grab the connection string from config, and replace the {DataDirectory} tag if we have a filepath to the DB...
            return configuration.GetSection("XeroProducts:ConnectionStrings")[connectionName]?
                                    .Replace("{DataDirectory}", AppContext.BaseDirectory + "App_Data");
        }

        /// <summary>
        /// Returns the default connection string
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string? GetDefaultConnectionString(IConfiguration configuration)
        {
            return GetConnectionString(configuration, "Default");

        }
    }
}
