using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Dtos;
using XeroProducts.BL.Identity;
using XeroProducts.BL.UnitTests.Mocks;
using XeroProducts.BL.UnitTests.TestData;
using XeroProducts.DAL.Interfaces;
using XeroProducts.PasswordService.Interfaces;

namespace XeroProducts.BL.UnitTests.Identity
{
    internal class UserProviderTests
    {
        [Test]
        public async Task CreateUser_WhenGivenValidDto_CreatesUserAndReturnsId()
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetMock(testData);
            

            var newUser = new UserDto()
            {
                FirstName = "Joe",
                LastName = "Bloggs",
                Email = "joe.bloggs@acme.com",
                Username = "jbloggs",
                Password = "password1"
            };

            var sut = new UserProvider(iPasswordProviderMock.Object, iUserDALProviderMock.Object);

            //Act
            var result = await sut.CreateUser(newUser);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result);
            Assert.AreNotEqual(originalTestDataCount, testData.Count);
            Assert.AreEqual(originalTestDataCount + 1, testData.Count);
        }
    }
}
