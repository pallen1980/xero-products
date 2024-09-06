using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.DAL.EntityFramework.Sql.Contexts;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.DAL.EntityFramework.Sql.Providers
{
    public class UserEntityFrameworkSqlProvider : BaseEntityFrameworkSqlProvider, IUserDALProvider
    {
        public UserEntityFrameworkSqlProvider(IXeroProductsContext context) : base(context)
        {

        }

        public Task<Guid> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}
