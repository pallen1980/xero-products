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
        public static byte[] GenerateKeyBytes(int byteCount = 32)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[byteCount];
                
                rng.GetBytes(key);

                return key;
            }
        }

        public static string GenerateKey(int byteCount = 32)
        {
            return Convert.ToBase64String(GenerateKeyBytes(byteCount));
        }
    }
}
