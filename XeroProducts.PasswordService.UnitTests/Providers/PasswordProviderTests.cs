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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.InstanceOf<byte[]>());
            Assert.That(result, Has.Length.EqualTo(512));
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.InstanceOf<string>());
        }
    }
}