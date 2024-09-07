namespace XeroProducts.KeyGeneration.UnitTests
{
    internal class KeyGenerationTests
    {

        [Test]
        public void GenerateKeyBytes_WhenNoByteCountGiven_ReturnsValidKey()
        {
            //Arrange

            //Act
            var result = KeyGenerator.GenerateKeyBytes();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.InstanceOf<byte[]>());
            Assert.That(result, Has.Length.EqualTo(32));
        }

        [Test]
        [TestCase(32)]
        [TestCase(64)]
        [TestCase(128)]
        [TestCase(256)]
        [TestCase(512)]
        public void GenerateKeyBytes_WhenByteCountGiven_ReturnsValidKey(int byteCount)
        {
            //Arrange

            //Act
            var result = KeyGenerator.GenerateKeyBytes(byteCount);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.InstanceOf<byte[]>());
            Assert.That(byteCount, Is.EqualTo(result.Length));
        }

        [Test]
        public void GenerateKey_WhenRun_ReturnsValidKey()
        {
            //Arrange

            //Act
            var result = KeyGenerator.GenerateKey();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.InstanceOf<string>());
        }
    }
}