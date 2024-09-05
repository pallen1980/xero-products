using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.KeyGenerator
{
    public class KeyGenerator
    {
        public static string GenerateKey(int noOfBytes = 32)
        {
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();

            byte[] key = new byte[noOfBytes];
            rng.GetBytes(key);

            return Convert.ToBase64String(key);
        }
    }
}
