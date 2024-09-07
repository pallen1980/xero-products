using Moq;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.BL.UnitTests.Mocks
{
    internal interface IUserDALProviderMock
    {
        public static Lazy<IUserDALProvider> GetLazyMock(List<User> testData)
            => new Lazy<IUserDALProvider>(GetMock(testData).Object);

        public static Mock<IUserDALProvider> GetMock(List<User> testData)
        {
            var mock = new Mock<IUserDALProvider>();

            mock.Setup(m => m.UserExists(It.IsAny<string>()))
                .Returns((string username) => 
                { 
                    return Task.FromResult(testData.Any(p => p.Username.Trim().ToLower() == username.Trim().ToLower())); 
                });

            mock.Setup(m => m.CreateUser(It.IsAny<User>()))
                .Returns((User u) =>
                {
                    testData.Add(u);

                    return Task.FromResult(u.Id);
                });

            mock.Setup(m => m.GetUser(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(testData.SingleOrDefault(td => td.Id == id)));

            mock.Setup(m => m.GetUser(It.IsAny<string>()))
                .Returns((string username) => Task.FromResult(testData.SingleOrDefault(td => td.Username.Trim().ToLower() == username.Trim().ToLower())));

            mock.Setup(m => m.UpdateUser(It.IsAny<User>()))
                .Callback((User u) =>
                {
                    var item = testData.Single(td => td.Id == u.Id);

                    item.FirstName = u.FirstName;
                    item.LastName = u.LastName;
                    item.Email = u.Email;
                    item.Username = u.Username;
                    item.HashedPassword = u.HashedPassword;
                    item.Salt = u.Salt;
                });

            mock.Setup(m => m.DeleteUser(It.IsAny<Guid>()))
                .Callback((Guid id) => testData.RemoveAll(td => td.Id == id));

            return mock;
        }
    }
}
