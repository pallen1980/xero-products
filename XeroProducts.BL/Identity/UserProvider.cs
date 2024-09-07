using System.Text;
using XeroProducts.BL.CustomExceptions;
using XeroProducts.BL.Dtos.User;
using XeroProducts.BL.Interfaces;
using XeroProducts.DAL.Interfaces;
using XeroProducts.PasswordService.Interfaces;

namespace XeroProducts.BL.Identity
{
    public class UserProvider : IUserProvider
    {
        private readonly Lazy<IPasswordProvider> _passwordProvider;
        private readonly Lazy<IUserDALProvider> _userDALProvider;

        protected IPasswordProvider PasswordProvider => _passwordProvider.Value;
        protected IUserDALProvider UserDALProvider => _userDALProvider.Value;

        public UserProvider(Lazy<IPasswordProvider> passwordProvider, 
                            Lazy<IUserDALProvider> userDALProvider)
        {
            _passwordProvider = passwordProvider;
            _userDALProvider = userDALProvider;
        }

        public virtual async Task<UserDto> GetUser(Guid id)
        {
            var user = await UserDALProvider.GetUser(id);

            return user == null ?
                throw new KeyNotFoundException($"No matching user found with ID: {id}")
                : new UserDto(user);
        }

        public virtual async Task<Guid> CreateUser(UserDto userDto)
        {
            //Check user does not already exist
            if (await UserDALProvider.UserExists(userDto.Username))
            {
                throw new AlreadyExistsException("Create Failed: The given username matches an existing user");
            }

            //generate unique salt for this user...
            var salt = PasswordProvider.GenerateSalt();

            //combine given password and salt to get hashed password
            var hashedPassword = PasswordProvider.HashPassword(userDto.Password, salt);

            //need to convert salt to string to save it
            string base64Salt = Convert.ToBase64String(salt);

            return await UserDALProvider.CreateUser(new Types.User
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

        public virtual async Task UpdateUser(UserDto userDto)
        {
            var user = await UserDALProvider.GetUser(userDto.Id);

            // Check user exists
            if (user == null)
            {
                throw new KeyNotFoundException($"Update Failed: No matching user found with ID {userDto.Id}");
            }

            //dont allow the super admin to be updated
            if (user.IsSuperAdmin)
            {
                throw new SuperAdminDeletionAttemptException("Update Failed: You cannot update the super admin account. This risks the system becoming un-usable.");
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;

            // if username is different, check that new username doesnt already exist
            if (user.Username != userDto.Username)
            {
                if (await UserDALProvider.UserExists(userDto.Username))
                {
                    throw new AlreadyExistsException("Update Failed: Username already exists.");
                }

                user.Username = userDto.Username;
            }

            // if the password is different, generate a new salt and hash & update both the new salt and password
            if (!PasswordsMatch(user.HashedPassword, user.Salt, userDto.Password))
            {
                //generate a new salt
                var salt = PasswordProvider.GenerateSalt();

                //combine given password and salt to get hashed password
                user.HashedPassword = PasswordProvider.HashPassword(userDto.Password, salt);

                //convert and update salt 
                user.Salt = Convert.ToBase64String(salt);
            }

            await UserDALProvider.UpdateUser(user);
        }

        public virtual async Task DeleteUser(Guid id)
        {
            var user = await UserDALProvider.GetUser(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"Delete Failed: No matching user found with ID: {id}");
            }

            if (user.IsSuperAdmin) 
            {
                throw new SuperAdminDeletionAttemptException("Delete Failed: You cannot remove the super admin account. This risks the system becoming un-usable.");
            }

            await UserDALProvider.DeleteUser(id);
        }

        /// <summary>
        /// Check whether the username and password match against a user's credential in the system.
        /// If they do, return the user, if not, return null
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual async Task<UserDto?> VerifyUserCredentials(string username, string password)
        {
            // Attempt to match the user on username
            var user = await UserDALProvider.GetUser(username);

            // if there is not matching username, return null (give less feedback when verifying usernames/passwords in the system)
            if (user == null)
            {
                return null;
            }

            // if the passwords don't match...
            if (!PasswordsMatch(user.HashedPassword, user.Salt, password))
            {
                return null;
            }

            // Username/Password match, return the user
            return new UserDto(user);
        }

        /// <summary>
        /// Hash the new password and existing salt together and compare the result with the existing password. Return the boolean result.
        /// </summary>
        /// <param name="originalHashedPassword"></param>
        /// <param name="originalHashedSalt"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        private bool PasswordsMatch(string originalHashedPassword, string originalHashedSalt, string newPassword)
        {
            return PasswordsMatch(originalHashedPassword, originalHashedSalt, newPassword, out _); //discard the out param (we dont need if with this method)
        }

        /// <summary>
        /// Hash the new password and existing salt together and compare the result with the existing password. Return the boolean result.
        /// </summary>
        /// <param name="originalHashedPassword"></param>
        /// <param name="originalHashedSalt"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        private bool PasswordsMatch(string originalHashedPassword, string originalHashedSalt, string newPassword, out string newHashedPassword)
        {
            // Convert the stored salt and newly entered password to byte arrays
            var storedSaltBytes = Convert.FromBase64String(originalHashedSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(newPassword);

            // Concatenate entered password and stored salt
            var saltedPassword = new byte[passwordBytes.Length + storedSaltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, passwordBytes.Length, storedSaltBytes.Length);

            // Hash the concatenated value
            newHashedPassword = PasswordProvider.HashPassword(newPassword, storedSaltBytes);

            // Compare the entered password hash with the stored hash
            if (newHashedPassword != originalHashedPassword)
            {
                //Password does not match
                return false;
            }

            //Passwords match
            return true;
        }
    }
}
