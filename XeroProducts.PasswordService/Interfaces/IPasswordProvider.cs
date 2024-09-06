using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.PasswordService.Interfaces
{
    public interface IPasswordProvider
    {
        public byte[] GenerateSalt();

        public string HashPassword(string password, byte[] salt);
    }
}
