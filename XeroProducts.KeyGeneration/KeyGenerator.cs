using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.KeyGeneration
{
    public class KeyGenerator
    {
        /// <summary>
        /// Generate a random sequence of values. the value returned will have the same length in bytes as the given argument
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static byte[] GenerateKeyBytes(int byteCount = 32)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[byteCount];
                
                rng.GetBytes(key);

                return key;
            }
        }

        /// <summary>
        /// Generate a cryptographic random number and format it to base64
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static string GenerateKey(int byteCount = 32)
        {
            return Convert.ToBase64String(GenerateKeyBytes(byteCount));
        }
    }
}
