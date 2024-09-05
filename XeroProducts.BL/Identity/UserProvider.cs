using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Dtos;
using XeroProducts.BL.Interfaces;

namespace XeroProducts.BL.Identity
{
    public class UserProvider : IUserProvider
    {
        public Task<Guid> CreateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> VerifyUserCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
