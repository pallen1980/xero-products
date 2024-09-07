using XeroProducts.BL.CustomExceptions;
using XeroProducts.BL.Dtos.User;
using XeroProducts.BL.Identity;
using XeroProducts.BL.UnitTests.Mocks;
using XeroProducts.BL.UnitTests.TestData;

namespace XeroProducts.BL.UnitTests.Identity
{
    internal class UserProviderTests
    {
        [Test]
        public async Task GetUserById_WhenUserWithIdExists_ReturnsUser()
        {
            //Arrange
            var testData = UserTestData.Create();

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);

            var id = testData.First().Id;

            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act
            var result = await sut.GetUser(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(id, Is.EqualTo(result.Id));
        }

        [Test]
        public void GetUserById_WhenUserWithIdDoesNotExists_RaisesException()
        {
            //Arrange
            var testData = UserTestData.Create();

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);

            var id = Guid.Empty;

            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act/Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => sut.GetUser(id));
        }

        [Test]
        public async Task CreateUser_WhenGivenValidDto_CreatesUserAndReturnsId()
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);
            
            var newUser = new UserDto()
            {
                FirstName = "Joe",
                LastName = "Bloggs",
                Email = "joe.bloggs@acme.com",
                Username = "jbloggs",
                Password = "password1"
            };

            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act
            var result = await sut.CreateUser(newUser);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(Guid.Empty, Is.Not.EqualTo(result));
            Assert.That(originalTestDataCount, Is.Not.EqualTo(testData.Count));
            Assert.That(originalTestDataCount + 1, Is.EqualTo(testData.Count));
        }

        [Test]
        public void CreateUser_WhenUsernameExists_RaisesException()
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

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);

            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act/Assert
            Assert.ThrowsAsync<AlreadyExistsException>(() => sut.CreateUser(newUser));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]

        public async Task UpdateUser_WhenGivenValidDto_UpdatesUser(int elementIndex)
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);

            var existingUser = new UserDto()
            {
                Id = testData.ElementAt(elementIndex).Id,
                FirstName = "Joe",
                LastName = "Bloggs",
                Email = "joe.bloggs@acme.com",
                Username = testData.ElementAt(elementIndex).Username,
                Password = "password1"
            };

            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act
            await sut.UpdateUser(existingUser);

            //Assert
            Assert.That(originalTestDataCount, Is.EqualTo(testData.Count));
            Assert.That(existingUser.FirstName, Is.EqualTo(testData.ElementAt(elementIndex).FirstName));
            Assert.That(existingUser.LastName, Is.EqualTo(testData.ElementAt(elementIndex).LastName));
            Assert.That(existingUser.Email, Is.EqualTo(testData.ElementAt(elementIndex).Email));
            Assert.That(existingUser.Username, Is.EqualTo(testData.ElementAt(elementIndex).Username));
        }

        [Test]
        public void UpdateUser_WhenUsernameDoesNotExist_RaisesException()
        {
            //Arrange
            var testData = UserTestData.Create();

            var existingUser = new UserDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "Joe",
                LastName = "Bloggs",
                Email = "joe.bloggs@acme.com",
                Username = "NOT_A_MATCHING_USERNAME",
                Password = "password1"
            };

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);

            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act/Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => sut.UpdateUser(existingUser));
        }

        [Test]
        public async Task DeleteUser_WhenUserExists_RemovesUser()
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);

            var id = testData.First(p => !p.IsSuperAdmin).Id;
            
            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act
            await sut.DeleteUser(id);

            //Assert
            Assert.That(originalTestDataCount, Is.Not.EqualTo(testData.Count));
            Assert.That(originalTestDataCount - 1, Is.EqualTo(testData.Count));
        }

        [Test]
        public void DeleteUser_WhenUserIsSuperAdmin_RaisesException()
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);

            var id = testData.First(p => p.IsSuperAdmin).Id;

            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act/Assert
            Assert.ThrowsAsync<SuperAdminDeletionAttemptException>(() => sut.DeleteUser(id));
        }

        [Test]
        public void DeleteUser_WhenUserDoesNotExist_RaisesException()
        {
            //Arrange
            var testData = UserTestData.Create();
            var originalTestDataCount = testData.Count;

            var iPasswordProviderMock = IPasswordProviderMock.GetLazyMock();
            var iUserDALProviderMock = IUserDALProviderMock.GetLazyMock(testData);

            var id = Guid.Empty;

            var sut = new UserProvider(iPasswordProviderMock, iUserDALProviderMock);

            //Act/Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => sut.DeleteUser(id));
        }
    }
}
