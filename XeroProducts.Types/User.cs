using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.Types
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public bool IsSuperAdmin { get; set; }
    }
}
