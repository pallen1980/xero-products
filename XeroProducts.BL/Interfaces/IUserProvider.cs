using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Dtos.User;

namespace XeroProducts.BL.Interfaces
{
    public interface IUserProvider
    {
        /// <summary>
        /// Return the user matching the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto> GetUser(Guid id);

        /// <summary>
        /// Create a new user and persist it to storage. Then return the ID of the created user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Guid> CreateUser(UserDto user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteUser(Guid Id);

        /// <summary>
        /// Verify the credentials match an existing user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserDto?> VerifyUserCredentials(string username, string password);
    }
}
