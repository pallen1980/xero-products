using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// returns whether a user with the given username exists against the context
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> UserExists(string username)
        {
            return await Context.Users.AnyAsync(u => u.Username == username);
        }

        /// <summary>
        /// Returns the user from the context that matches the given ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<User?> GetUser(Guid Id)
        {
            return await Context.Users.SingleOrDefaultAsync(u => u.Id == Id);
        }

        /// <summary>
        /// Returns the user from the context that matches the given username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User?> GetUser(string username)
        {
            return await Context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Adds/Attaches the given user to the context, and saves the context
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Guid> CreateUser(User user)
        {
            await Context.Users.AddAsync(user);

            await Context.SaveChangesAsync();

            return user.Id;
        }

        /// <summary>
        /// Finds the user in the context, updates the changeable properties, and saves the context
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task UpdateUser(User user)
        {
            var dbUser = await Context.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

            if (dbUser == null)
            {
                throw new KeyNotFoundException($"Update Failed: No User found matching id: {user.Id}");
            }

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.Username = user.Username;
            dbUser.HashedPassword = user.HashedPassword;
            dbUser.Salt = user.Salt;

            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Find the user in the context, removes it, and saves the context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeleteUser(Guid id)
        {
            var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Delete Failed: No User found matching id: {id}");
            }

            Context.Users.Remove(user);

            Context.SaveChanges();
        }
    }
}
