using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Providers;
using XeroProducts.BL.UnitTests.Mocks;
using XeroProducts.Types;

namespace XeroProducts.BL.UnitTests.Providers
{
    internal class ProductOptionProviderTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetProductOption_WhenOptionExists_ReturnsOption()
        {
            //Arrange
            var testData = CreateTestData();

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetMock(testData);

            var expectedItem = testData.FirstOrDefault();

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock.Object);


            //Act
            var result = await sut.GetProductOption(expectedItem.Id);


            //Assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<ProductOption>(result);
            Assert.AreEqual(expectedItem.Id, result.Id);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetProductOptions_WhenProductHasOptions_ReturnsOptions(int elementIndex)
        {
            //Arrange
            var testData = CreateTestData();

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetMock(testData);

            var productId = testData.Select(td => td.ProductId).Distinct().ElementAt(elementIndex);

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock.Object);


            //Act
            var result = await sut.GetProductOptions(productId);


            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Items);
            Assert.IsNotEmpty(result.Items);
            Assert.AreEqual(testData.Where(p => p.ProductId == productId).Count(), result.Items.Count);
        }

        [Test]
        public async Task Save_WhenOptionDoesNotExist_StoresNewOption()
        {
            //Arrange
            var testData = CreateTestData();
            var initialTestDataCount = testData.Count;

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetMock(testData);

            var newOption = new ProductOption()
            {
                Id = Guid.NewGuid(),
                Name = "New Option",
                Description = "New Description",
                ProductId = Guid.NewGuid()
            };

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock.Object);


            //Act
            await sut.Save(newOption);


            //Assert
            Assert.AreNotEqual(initialTestDataCount, testData.Count);
            Assert.AreEqual(initialTestDataCount + 1, testData.Count);
            Assert.IsNotNull(testData.SingleOrDefault(td => td.Id == newOption.Id));
            Assert.AreEqual(newOption.Name, testData.SingleOrDefault(td => td.Id == newOption.Id).Name);
            Assert.AreEqual(newOption.Description, testData.SingleOrDefault(td => td.Id == newOption.Id).Description);
            Assert.AreEqual(newOption.ProductId, testData.SingleOrDefault(td => td.Id == newOption.Id).ProductId);
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        public async Task Save_WhenOptionExists_UpdatesExistingOption(int elementIndex)
        {
            //Arrange
            var testData = CreateTestData();
            var initialTestDataCount = testData.Count;

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetMock(testData);

            var existingOption = new ProductOption(false)
            {
                Id = testData.ElementAt(elementIndex).Id,
                Name = "Updated Option",
                Description = "Updated Description",
            };

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock.Object);


            //Act
            await sut.Save(existingOption);


            //Assert
            Assert.AreEqual(initialTestDataCount, testData.Count);
            Assert.IsNotNull(testData.SingleOrDefault(td => td.Id == existingOption.Id));
            Assert.AreEqual(existingOption.Name, testData.SingleOrDefault(td => td.Id == existingOption.Id).Name);
            Assert.AreEqual(existingOption.Description, testData.SingleOrDefault(td => td.Id == existingOption.Id).Description);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(7)]
        public async Task DeleteProductOption_WhenProductExists_RemovesOption(int elementIndex)
        {
            //Arrange
            var testData = CreateTestData();
            var initialTestDataCount = testData.Count;

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetMock(testData);

            var id = testData.ElementAt(elementIndex).Id;

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock.Object);


            //Act
            await sut.Delete(id);


            //Assert
            Assert.AreNotEqual(initialTestDataCount, testData.Count);
            Assert.AreEqual(initialTestDataCount - 1, testData.Count);
            Assert.IsNull(testData.SingleOrDefault(td => td.Id == id));
        }




        private List<ProductOption> CreateTestData()
        {
            var productIds = new List<Guid>()
            { 
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            return new List<ProductOption>
            {
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 1 - Option 1", Description = "Test Description", ProductId = productIds.FirstOrDefault() },
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 1 - Option 2", Description = "Test Description", ProductId = productIds.FirstOrDefault() },
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 1 - Option 3", Description = "Test Description", ProductId = productIds.FirstOrDefault() },
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 2 - Option 1", Description = "Test Description", ProductId = productIds.ElementAt(1) },
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 2 - Option 2", Description = "Test Description", ProductId = productIds.ElementAt(1) },
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 3 - Option 1", Description = "Test Description", ProductId = productIds.ElementAt(2) },
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 3 - Option 2", Description = "Test Description", ProductId = productIds.ElementAt(2) },
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 3 - Option 3", Description = "Test Description", ProductId = productIds.ElementAt(2) },
                new ProductOption { Id = Guid.NewGuid(), Name = "Product 3 - Option 4", Description = "Test Description", ProductId = productIds.ElementAt(2) },
            };
        }
    }
}
