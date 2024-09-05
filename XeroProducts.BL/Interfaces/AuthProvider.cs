using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.BL.Interfaces
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
