using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.BL.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsSuperAdmin { get; set; }

        public UserDto()
        {
            Id = Guid.Empty;
            FirstName = "";
            LastName = "";
            Email = "";
            Username = "";
            Password = "";
            Salt = "";
            IsSuperAdmin = false;
        }

        public UserDto(Types.User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Username = user.Username;
            Password = user.HashedPassword;
            Salt = user.Salt;
            IsSuperAdmin = user.IsSuperAdmin;
        }
    }
}
