using Moq;
using System.Security.Cryptography;
using XeroProducts.PasswordService.Interfaces;

namespace XeroProducts.BL.UnitTests.Mocks
{
    internal interface IPasswordProviderMock
    {
        public static Lazy<IPasswordProvider> GetLazyMock()
            => new Lazy<IPasswordProvider>(GetMock().Object);

        public static Mock<IPasswordProvider> GetMock()
        {
            var mock = new Mock<IPasswordProvider>();

            mock.Setup(m => m.GenerateSalt())
                .Returns(() => GetRandomBytes());

            mock.Setup(m => m.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns((string pass, byte[] salt) => ConcatStringAndByteArr(pass, salt));

            return mock;
        }

        private static byte[] GetRandomBytes()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[512];

                rng.GetBytes(key);

                return key;
            }
        }

        private static string ConcatStringAndByteArr(string str, byte[] bytes)
        {
            return str + "|" + Convert.ToBase64String(bytes);
        }
    }
}
