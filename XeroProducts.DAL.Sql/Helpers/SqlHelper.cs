using Microsoft.Data.SqlClient;
using System.Reflection;

namespace XeroProducts.DAL.Sql.Helpers
{
    public class SqlHelper
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";
        
        public static SqlConnection NewConnection()
        {
            //var connstr = ConnectionString.Replace("{DataDirectory}", System.Web.HttpContext.Current.Server.MapPath("~/App_Data"));
            var connstr = ConnectionString.Replace("{DataDirectory}", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\App_Data");

            return new SqlConnection(connstr);
        }
    }
}