using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.CustomExceptions;
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
        public async Task GetUserById_WhenUserWithIdExists_ReturnsUser()
        {
            //Arrange
            var testData = UserTestData.Create();

            var iPasswordProviderMock = IPasswordProviderMock.GetMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetMock(testData);

            var id = testData.First().Id;

            var sut = new UserProvider(iPasswordProviderMock.Object, iUserDALProviderMock.Object);

            //Act
            var result = await sut.GetUser(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public async Task GetUserById_WhenUserWithIdDoesNotExists_RaisesException()
        {
            //Arrange
            var testData = UserTestData.Create();

            var iPasswordProviderMock = IPasswordProviderMock.GetMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetMock(testData);

            var id = Guid.Empty;

            var sut = new UserProvider(iPasswordProviderMock.Object, iUserDALProviderMock.Object);

            //Act/Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => sut.GetUser(id));
        }

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

        [Test]
        public async Task CreateUser_WhenUsernameExists_RaisesException()
        {
            //Arrange
            var duplicateUserName = "ADuplicateUserName";

            var testData = new List<Types.User>()
            {
                new Types.User() { Id = Guid.NewGuid(), Username = duplicateUserName.ToString() }
            };

            var newUser = new UserDto()
            {
                FirstName = "Joe",
                LastName = "Bloggs",
                Email = "joe.bloggs@acme.com",
                Username = duplicateUserName.ToString(),
                Password = "password1"
            };

            var iPasswordProviderMock = IPasswordProviderMock.GetMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetMock(testData);

            var sut = new UserProvider(iPasswordProviderMock.Object, iUserDALProviderMock.Object);

            //Act/Assert
            Assert.ThrowsAsync<AlreadyExistsException>(() => sut.CreateUser(newUser));
        }

        [Test]
        public async Task DeleteUser_WhenUserExists_RemovesUser()
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetMock(testData);

            var id = testData.First(p => !p.IsSuperAdmin).Id;
            
            var sut = new UserProvider(iPasswordProviderMock.Object, iUserDALProviderMock.Object);

            //Act
            await sut.DeleteUser(id);

            //Assert
            Assert.AreNotEqual(originalTestDataCount, testData.Count);
            Assert.AreEqual(originalTestDataCount - 1, testData.Count);
        }

        [Test]
        public async Task DeleteUser_WhenUserIsSuperAdmin_RaisesException()
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetMock(testData);

            var id = testData.First(p => p.IsSuperAdmin).Id;

            var sut = new UserProvider(iPasswordProviderMock.Object, iUserDALProviderMock.Object);

            //Act/Assert
            Assert.ThrowsAsync<SuperAdminDeletionAttemptException>(() => sut.DeleteUser(id));
        }

        [Test]
        public async Task DeleteUser_WhenUserDoesNotExist_RaisesException()
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetMock(testData);

            var id = Guid.Empty;

            var sut = new UserProvider(iPasswordProviderMock.Object, iUserDALProviderMock.Object);

            //Act/Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => sut.DeleteUser(id));
        }
    }
}
