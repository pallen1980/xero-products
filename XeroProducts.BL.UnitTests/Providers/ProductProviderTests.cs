using Moq;
using XeroProducts.BL.Dtos.Product;
using XeroProducts.BL.Interfaces;
using XeroProducts.BL.Providers;
using XeroProducts.BL.UnitTests.Mocks;
using XeroProducts.BL.UnitTests.TestData;

namespace XeroProducts.BL.UnitTests.Providers
{
    internal class ProductProviderTests
    {
        [Test]
        public async Task GetProduct_WhenExists_ReturnsProduct()
        {
            //Arrange
            var testData = ProductTestData.Create();

            var iProductDALProviderMock = IProductDALProviderMock.GetMock(testData);
            var iProductOptionProviderMock = new Mock<IProductOptionProvider>();

            var expectedItem = testData.FirstOrDefault();

            var sut = new ProductProvider(iProductDALProviderMock.Object, iProductOptionProviderMock.Object);


            //Act
            var result = await sut.GetProduct(expectedItem.Id);


            //Assert
            Assert.IsNotNull(result);
            Assert.IsAssignableFrom<ProductDto>(result);
            Assert.That(expectedItem.Id, Is.EqualTo(result.Id));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetProducts_WhenNameMatches_ReturnsMatchingProducts(int elementIndex)
        {
            //Arrange
            var testData = ProductTestData.Create();

            var iProductDALProviderMock = IProductDALProviderMock.GetMock(testData);
            var iProductOptionProviderMock = new Mock<IProductOptionProvider>();

            var expectedItem = testData.ElementAt(elementIndex);

            var sut = new ProductProvider(iProductDALProviderMock.Object, iProductOptionProviderMock.Object);


            //Act
            var result = await sut.GetProducts(expectedItem.Name);


            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.That(testData.Where(p => p.Name.Contains(expectedItem.Name)).Count(), Is.EqualTo(result.Count));
        }

        [Test]
        public async Task Save_WhenOptionDoesNotExist_StoresNewOption()
        {
            //Arrange
            var testData = ProductTestData.Create();
            var initialTestDataCount = testData.Count;

            var iProductDALProviderMock = IProductDALProviderMock.GetMock(testData);
            var iProductOptionProviderMock = new Mock<IProductOptionProvider>();

            var newProduct = new ProductDto()
            {
                Id = Guid.NewGuid(),
                Name = "New Product",
                Description = "New Description",
                Price = 200.0M,
                DeliveryPrice = 2.0M
            };

            var sut = new ProductProvider(iProductDALProviderMock.Object, iProductOptionProviderMock.Object);


            //Act
            await sut.Save(newProduct);


            //Assert
            Assert.That(initialTestDataCount, Is.Not.EqualTo(testData.Count));
            Assert.That(initialTestDataCount + 1, Is.EqualTo(testData.Count));
            Assert.IsNotNull(testData.SingleOrDefault(td => td.Id == newProduct.Id));
            Assert.That(newProduct.Name, Is.EqualTo(testData.SingleOrDefault(td => td.Id == newProduct.Id).Name));
            Assert.That(newProduct.Description, Is.EqualTo(testData.SingleOrDefault(td => td.Id == newProduct.Id).Description));
            Assert.That(newProduct.Price, Is.EqualTo(testData.SingleOrDefault(td => td.Id == newProduct.Id).Price));
            Assert.That(newProduct.DeliveryPrice, Is.EqualTo(testData.SingleOrDefault(td => td.Id == newProduct.Id).DeliveryPrice));
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        public async Task Save_WhenOptionExists_UpdatesExistingOption(int elementIndex)
        {
            //Arrange
            var testData = ProductTestData.Create(10);
            var initialTestDataCount = testData.Count;

            var iProductDALProviderMock = IProductDALProviderMock.GetMock(testData);
            var iProductOptionProviderMock = new Mock<IProductOptionProvider>();

            var existingProduct = new ProductDto(false)
            {
                Id = testData.ElementAt(elementIndex).Id,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 200.0M,
                DeliveryPrice = 2.0M
            };

            var sut = new ProductProvider(iProductDALProviderMock.Object, iProductOptionProviderMock.Object);


            //Act
            await sut.Save(existingProduct);


            //Assert
            Assert.That(initialTestDataCount, Is.EqualTo(testData.Count));
            Assert.IsNotNull(testData.SingleOrDefault(td => td.Id == existingProduct.Id));
            Assert.That(existingProduct.Name, Is.EqualTo(testData.SingleOrDefault(td => td.Id == existingProduct.Id).Name));
            Assert.That(existingProduct.Description, Is.EqualTo(testData.SingleOrDefault(td => td.Id == existingProduct.Id).Description));
        }


        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(7)]
        public async Task DeleteProduct_WhenExists_RemovesProduct(int elementIndex)
        {
            //Arrange
            var productsTestData = ProductTestData.Create(10);
            var initialProductTestDataCount = productsTestData.Count;
            var productOptionsTestData = ProductOptionTestData.Create(productsTestData.Select(p => p.Id).ToList());
            var initialOptionsTestDataCount = productOptionsTestData.Count;

            var iProductDALProviderMock = IProductDALProviderMock.GetMock(productsTestData);
            var iProductOptionDALProviderMock = IProductOptionDALProviderMock.GetMock(productOptionsTestData);
            var iProductOptionProvider = new ProductOptionProvider(iProductOptionDALProviderMock.Object);

            var id = productsTestData.ElementAt(elementIndex).Id;

            var sut = new ProductProvider(iProductDALProviderMock.Object, iProductOptionProvider);


            //Act
            await sut.Delete(id);


            //Assert
            Assert.That(initialProductTestDataCount, Is.Not.EqualTo(productsTestData.Count));
            Assert.That(initialProductTestDataCount - 1, Is.EqualTo(productsTestData.Count));
            Assert.IsNull(productsTestData.SingleOrDefault(td => td.Id == id));

            Assert.That(initialOptionsTestDataCount, Is.Not.EqualTo(productOptionsTestData.Count));
            Assert.IsNull(productOptionsTestData.SingleOrDefault(td => td.ProductId == id));
        }


    }
}
