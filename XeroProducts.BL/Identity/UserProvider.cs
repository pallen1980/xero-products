using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.CustomExceptions;
using XeroProducts.BL.Dtos;
using XeroProducts.BL.Interfaces;
using XeroProducts.DAL.Interfaces;
using XeroProducts.PasswordService;
using XeroProducts.PasswordService.Interfaces;

namespace XeroProducts.BL.Identity
{
    public class UserProvider : IUserProvider
    {
        private readonly IPasswordProvider _passwordProvider;
        private readonly IUserDALProvider _userDALProvider;

        public UserProvider(IPasswordProvider passwordProvider, 
                            IUserDALProvider userDALProvider)
        {
            _passwordProvider = passwordProvider;
            _userDALProvider = userDALProvider;
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            var user = await _userDALProvider.GetUser(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"No matching user found with ID: {id}");
            }

            return new UserDto(user);
        }

        public async Task<Guid> CreateUser(UserDto userDto)
        {
            //Check user does not already exist
            if (await _userDALProvider.UserExists(userDto.Username))
            {
                throw new AlreadyExistsException("The given details match an existing user");
            }

            //generate unique salt for this user...
            var salt = _passwordProvider.GenerateSalt();

            //combine given password and salt to get hashed password
            var hashedPassword = _passwordProvider.HashPassword(userDto.Password, salt);

            //need to convert salt to string to save it
            string base64Salt = Convert.ToBase64String(salt);

            //byte[] retrievedSaltBytes = Convert.FromBase64String(salt);

            return await _userDALProvider.CreateUser(new Types.User
            {
                Id = Guid.NewGuid(),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Username = userDto.Username,
                HashedPassword = hashedPassword,
                Salt = base64Salt
            });
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _userDALProvider.GetUser(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Delete Failed: No matching user found with ID: {id}");
            }

            if (user.IsSuperAdmin) 
            {
                throw new SuperAdminDeletionAttemptException("Delete Failed: You cannot remove the super admin account. This risks the system becoming un-usable.");
            }

            await _userDALProvider.DeleteUser(id);
        }

        public async Task<UserDto?> VerifyUserCredentials(string username, string password)
        {
            // Attempt to match the user on username
            var user = await _userDALProvider.GetUser(username);

            // If there is not matching username, return null (give less feedback when verifying usernames/passwords in the system)
            if (user == null)
            {
                return null;
            }

            // Convert the stored salt and entered password to byte arrays
            var storedSaltBytes = Convert.FromBase64String(user.Salt);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Concatenate entered password and stored salt
            var saltedPassword = new byte[passwordBytes.Length + storedSaltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, passwordBytes.Length, storedSaltBytes.Length);

            // Hash the concatenated value
            var enteredPasswordHash = _passwordProvider.HashPassword(password, storedSaltBytes);

            // Compare the entered password hash with the stored hash
            if (enteredPasswordHash != user.HashedPassword)
            {
                //Password does not match
                return null;
            }

            // Username/Password match, return the user
            return new UserDto(user);
        }
    }
}
