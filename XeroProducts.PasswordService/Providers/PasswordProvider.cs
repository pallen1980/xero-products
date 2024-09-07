using System.Text;
using XeroProducts.PasswordService.Interfaces;
using XeroProducts.KeyGeneration;
using XSystem.Security.Cryptography;

namespace XeroProducts.PasswordService.Providers
{
    public class PasswordProvider : IPasswordProvider
    {
        public virtual byte[] GenerateSalt()
        {
            return KeyGenerator.GenerateKeyBytes(512);
        }

        public virtual string HashPassword(string password, byte[] salt)
        {
            using (var sha512 = new SHA512Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = sha512.ComputeHash(saltedPassword);

                // Concatenate the salt and hashed password for storage
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }
    }
}
