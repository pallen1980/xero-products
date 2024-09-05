using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Dtos;
using XeroProducts.BL.Identity;

namespace XeroProducts.BL.UnitTests.Identity
{
    internal class UserProviderTests
    {
        [Test]
        public async Task CreateUser_WhenGivenValidDto_CreatesUserAndReturnsId()
        {
            //Arrange
            var newUser = new UserDto()
            {
                FirstName = "Joe",
                LastName = "Bloggs",
                Email = "joe.bloggs@acme.com",
                Username = "jbloggs",
                Password = "password1"
            };

            var sut = new UserProvider();

            //Act
            var result = await sut.CreateUser(newUser);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result);
        }
    }
}
