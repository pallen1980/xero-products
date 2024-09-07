using Moq;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.BL.UnitTests.Mocks
{
    internal interface IProductOptionDALProviderMock
    {
        public static Lazy<IProductOptionDALProvider> GetLazyMock(List<ProductOption> testData)
            => new Lazy<IProductOptionDALProvider>(GetMock(testData).Object);

        public static Mock<IProductOptionDALProvider> GetMock(List<ProductOption> testData)
        {
            var mock = new Mock<IProductOptionDALProvider>();

            //Mock method GetProductOption(id):-
            mock.Setup(m => m.GetProductOption(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(testData.SingleOrDefault(p => p.Id == id)));

            //Mock method GetProductOptions(productId):-
            mock.Setup(m => m.GetProductOptions(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(new ProductOptions(testData.Where(td => td.ProductId == id).ToList())));

            //Mock method CreateProductOption(option):-
            mock.Setup(m => m.CreateProductOption(It.IsAny<ProductOption>()))
                .Callback((ProductOption p) => testData.Add(p));

            //Mock method UpdateProductOption(option):-
            mock.Setup(m => m.UpdateProductOption(It.IsAny<ProductOption>()))
                .Callback((ProductOption p) => 
                {
                    var item = testData.Single(td => td.Id == p.Id);

                    item.Name = p.Name;
                    item.Description = p.Description;
                });

            //Mock method DeleteProductOption(id):-
            mock.Setup(m => m.DeleteProductOption(It.IsAny<Guid>()))
                .Callback((Guid id) => testData.RemoveAll(td => td.Id == id));

            return mock;
        }
    }
}
