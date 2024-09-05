using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.BL.Interfaces
{
    public interface IAuthProvider
    {
        public byte[] GenerateSalt();

        public string HashPassword(string password, byte[] salt);
    }
}
