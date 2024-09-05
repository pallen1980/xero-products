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
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.IsInstanceOf<byte[]>(result);
            Assert.AreEqual(32, result.Length);
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
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.IsInstanceOf<byte[]>(result);
            Assert.AreEqual(byteCount, result.Length);
        }

        [Test]
        public void GenerateKey_WhenRun_ReturnsValidKey()
        {
            //Arrange

            //Act
            var result = KeyGenerator.GenerateKey();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.IsInstanceOf<string>(result);
        }
    }
}