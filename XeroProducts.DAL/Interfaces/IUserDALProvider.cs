using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.DAL.Interfaces
{
    public interface IUserDALProvider
    {
        /// <summary>
        /// Check if a user with the given username already exists in the data store
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> UserExists(string username);

        /// <summary>
        /// Get a user by their ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<User> GetUser(Guid Id);

        /// <summary>
        /// Get a user by their username
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUser(string username);

        /// <summary>
        /// Store a new user in the data store
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Guid> CreateUser(User user);

        /// <summary>
        /// Update an existing user in the data store
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateUser(User user);

        /// <summary>
        /// Delete the user that matches the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteUser(Guid id);
    }
}
