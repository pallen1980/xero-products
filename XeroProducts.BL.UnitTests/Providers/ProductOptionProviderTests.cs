using System;
using XeroProducts.BL.Dtos.Product;
using XeroProducts.BL.Providers;
using XeroProducts.BL.UnitTests.Mocks;
using XeroProducts.BL.UnitTests.TestData;

namespace XeroProducts.BL.UnitTests.Providers
{
    internal class ProductOptionProviderTests
    {

        [Test]
        public async Task GetProductOption_WhenOptionExists_ReturnsOption()
        {
            //Arrange
            var testData = ProductOptionTestData.Create(3);

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetLazyMock(testData);

            var expectedItem = testData.First();

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock);


            //Act
            var result = await sut.GetProductOption(expectedItem.Id);


            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ProductOptionDto>());
            Assert.That(expectedItem.Id, Is.EqualTo(result.Id));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetProductOptions_WhenProductHasOptions_ReturnsOptions(int elementIndex)
        {
            //Arrange
            var testData = ProductOptionTestData.Create(3);

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetLazyMock(testData);

            var productId = testData.Select(td => td.ProductId).Distinct().ElementAt(elementIndex);

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock);


            //Act
            var result = await sut.GetProductOptions(productId);


            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(testData.Where(p => p.ProductId == productId).Count(), Is.EqualTo(result.Count));
        }

        [Test]
        public async Task Save_WhenOptionDoesNotExist_StoresNewOption()
        {
            //Arrange
            var testData = ProductOptionTestData.Create();
            var initialTestDataCount = testData.Count;

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetLazyMock(testData);

            var newOption = new ProductOptionDto()
            {
                Id = Guid.NewGuid(),
                Name = "New Option",
                Description = "New Description",
                ProductId = Guid.NewGuid()
            };

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock);


            //Act
            await sut.Save(newOption);


            //Assert
            Assert.That(initialTestDataCount, Is.Not.EqualTo(testData.Count));
            Assert.That(initialTestDataCount + 1, Is.EqualTo(testData.Count));
            Assert.That(testData.SingleOrDefault(td => td.Id == newOption.Id), Is.Not.Null);
            Assert.That(newOption.Name, Is.EqualTo(testData.SingleOrDefault(td => td.Id == newOption.Id).Name));
            Assert.That(newOption.Description, Is.EqualTo(testData.SingleOrDefault(td => td.Id == newOption.Id).Description));
            Assert.That(newOption.ProductId, Is.EqualTo(testData.SingleOrDefault(td => td.Id == newOption.Id).ProductId));
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        public async Task Save_WhenOptionExists_UpdatesExistingOption(int elementIndex)
        {
            //Arrange
            var testData = ProductOptionTestData.Create();
            var initialTestDataCount = testData.Count;

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetLazyMock(testData);

            var existingOption = new ProductOptionDto(false)
            {
                Id = testData.ElementAt(elementIndex).Id,
                Name = "Updated Option",
                Description = "Updated Description"
            };

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock);


            //Act
            await sut.Save(existingOption);


            //Assert
            Assert.That(initialTestDataCount, Is.EqualTo(testData.Count));
            Assert.That(testData.SingleOrDefault(td => td.Id == existingOption.Id), Is.Not.Null);
            Assert.That(existingOption.Name, Is.EqualTo(testData.SingleOrDefault(td => td.Id == existingOption.Id).Name));
            Assert.That(existingOption.Description, Is.EqualTo(testData.SingleOrDefault(td => td.Id == existingOption.Id).Description));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(7)]
        public async Task DeleteProductOption_WhenOptionExists_RemovesOption(int elementIndex)
        {
            //Arrange
            var testData = ProductOptionTestData.Create();
            var initialTestDataCount = testData.Count;

            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetLazyMock(testData);

            var id = testData.ElementAt(elementIndex).Id;

            var sut = new ProductOptionProvider(iProductOptionDALProviderMock);


            //Act
            await sut.Delete(id);


            //Assert
            Assert.That(initialTestDataCount, Is.Not.EqualTo(testData.Count));
            Assert.That(initialTestDataCount - 1, Is.EqualTo(testData.Count));
            Assert.That(testData.SingleOrDefault(td => td.Id == id), Is.Null);
        }
    }
}
