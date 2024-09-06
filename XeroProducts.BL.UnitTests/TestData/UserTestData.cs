using XeroProducts.PasswordService.Interfaces;
using XeroProducts.PasswordService.Providers;
using XeroProducts.Types;

namespace XeroProducts.BL.UnitTests.TestData
{
    internal class UserTestData
    {
        private readonly static IPasswordProvider _passwordProvider = new PasswordProvider();

        internal static List<User> Create(int noOfUsers = 5)
        {
            var products = new List<User>();

            for (var i = 0; i < noOfUsers; i++)
            {
                var salt = _passwordProvider.GenerateSalt();
                var hashedPassword = _passwordProvider.HashPassword("abc123", salt);
                var hashedSalt = Convert.ToBase64String(salt);

                products.Add(
                    new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = string.Format("Firstname{0}", i),
                        LastName = string.Format("Lastname{0}", i),
                        Email = string.Format("Email{0}", i),
                        Username = string.Format("Username{0}", i),
                        HashedPassword = hashedPassword,
                        Salt = hashedSalt,
                        IsSuperAdmin = (i == 0) //first user is super admin
                    });
            }

            return products;
        }
    }
}
