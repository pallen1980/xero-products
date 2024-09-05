using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Interfaces;

namespace XeroProducts.BL.Authentication
{
    public class AuthProvider : IAuthProvider
    {
        public AuthProvider() { }

        public byte[] GenerateSalt()
        {
            return KeyGenerator.KeyGenerator.GenerateKeyBytes(512);
        }

        public string HashPassword(string password, byte[] salt)
        {
            throw new NotImplementedException();
        }
    }
}
