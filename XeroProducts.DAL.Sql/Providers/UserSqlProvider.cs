using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.DAL.Sql.Providers
{
    public class UserSqlProvider : IUserDALProvider
    {
        public async Task<bool> UserExists(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
