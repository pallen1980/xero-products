using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Dtos;

namespace XeroProducts.BL.Interfaces
{
    public interface IUserProvider
    {
        /// <summary>
        /// Create a new user and persist it to storage. Then return the ID of the created user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Guid> CreateUser(UserDto user);

        /// <summary>
        /// Verify the credentials match an existing user. Return the ID of the user they match
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Guid> VerifyUserCredentials(string username, string password);
    }
}
