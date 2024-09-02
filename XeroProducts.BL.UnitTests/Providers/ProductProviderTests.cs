using Moq;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.BL.Interfaces;
using XeroProducts.BL.Providers;
using XeroProducts.BL.UnitTests.Mocks;
using XeroProducts.BL.UnitTests.TestData;
using XeroProducts.DAL;
using XeroProducts.Types;

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
            Assert.IsAssignableFrom<Product>(result);
            Assert.AreEqual(expectedItem.Id, result.Id);
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
            Assert.IsNotNull(result.Items);
            Assert.IsNotEmpty(result.Items);
            Assert.AreEqual(testData.Where(p => p.Name.Contains(expectedItem.Name)).Count(), result.Items.Count);
        }

        [Test]
        public async Task Save_WhenOptionDoesNotExist_StoresNewOption()
        {
            //Arrange
            var testData = ProductTestData.Create();
            var initialTestDataCount = testData.Count;

            var iProductDALProviderMock = IProductDALProviderMock.GetMock(testData);
            var iProductOptionProviderMock = new Mock<IProductOptionProvider>();

            var newProduct = new Product()
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
            Assert.AreNotEqual(initialTestDataCount, testData.Count);
            Assert.AreEqual(initialTestDataCount + 1, testData.Count);
            Assert.IsNotNull(testData.SingleOrDefault(td => td.Id == newProduct.Id));
            Assert.AreEqual(newProduct.Name, testData.SingleOrDefault(td => td.Id == newProduct.Id).Name);
            Assert.AreEqual(newProduct.Description, testData.SingleOrDefault(td => td.Id == newProduct.Id).Description);
            Assert.AreEqual(newProduct.Price, testData.SingleOrDefault(td => td.Id == newProduct.Id).Price);
            Assert.AreEqual(newProduct.DeliveryPrice, testData.SingleOrDefault(td => td.Id == newProduct.Id).DeliveryPrice);
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

            var existingProduct = new Product(false)
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
            Assert.AreEqual(initialTestDataCount, testData.Count);
            Assert.IsNotNull(testData.SingleOrDefault(td => td.Id == existingProduct.Id));
            Assert.AreEqual(existingProduct.Name, testData.SingleOrDefault(td => td.Id == existingProduct.Id).Name);
            Assert.AreEqual(existingProduct.Description, testData.SingleOrDefault(td => td.Id == existingProduct.Id).Description);
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
            Assert.AreNotEqual(initialProductTestDataCount, productsTestData.Count);
            Assert.AreEqual(initialProductTestDataCount - 1, productsTestData.Count);
            Assert.IsNull(productsTestData.SingleOrDefault(td => td.Id == id));

            Assert.AreNotEqual(initialOptionsTestDataCount, productOptionsTestData.Count);
            Assert.IsNull(productOptionsTestData.SingleOrDefault(td => td.ProductId == id));
        }


    }
}
