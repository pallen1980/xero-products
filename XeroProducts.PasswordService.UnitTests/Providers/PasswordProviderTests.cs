using XeroProducts.PasswordService.Providers;

namespace XeroProducts.PasswordService.UnitTests.Providers
{
    internal class PasswordProviderTests
    {
        [Test]
        public void GenerateSalt_WhenRun_ReturnsValidSalt()
        {
            //Arrange
            var sut = new PasswordProvider();

            //Act
            var result = sut.GenerateSalt();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.IsInstanceOf<byte[]>(result);
            Assert.AreEqual(512, result.Length);
        }

        [Test]
        public void HashPassword_WhenRun_ReturnsValidHashedPassword()
        {
            //Arrange
            var sut = new PasswordProvider();

            var password = "password1";
            var salt = sut.GenerateSalt();

            //Act
            var result = sut.HashPassword(password, salt);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.IsInstanceOf<string>(result);
        }
    }
}