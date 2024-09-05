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

        public Task<Guid> VerifyUserCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
