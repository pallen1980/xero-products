using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.BL.UnitTests.TestData
{
    internal class UserTestData
    {
        internal static List<User> Create(int noOfUsers = 5)
        {
            var products = new List<User>();

            for (var i = 0; i < noOfUsers; i++)
            {
                products.Add(
                    new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = string.Format("Firstname{0}", i),
                        LastName = string.Format("Lastname{0}", i),
                        Email = string.Format("Email{0}", i),
                        Username = string.Format("Username{0}", i),
                        HashedPassword = "abc123",
                        Salt = "def456"
                    });
            }

            return products;
        }
    }
}
