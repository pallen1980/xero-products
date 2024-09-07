using Moq;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.BL.UnitTests.Mocks
{
    internal interface IProductDALProviderMock
    {
        public static Lazy<IProductDALProvider> GetLazyMock(List<Product> testData) 
            => new Lazy<IProductDALProvider>(GetMock(testData).Object);
        
        public static Mock<IProductDALProvider> GetMock(List<Product> testData)
        {
            var mock = new Mock<IProductDALProvider>();

            //Mock method GetProduct(id):-
            mock.Setup(m => m.GetProduct(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(testData.Single(p => p.Id == id)));

            //Mock method GetProducts(name):-
            mock.Setup(m => m.GetProducts(It.IsAny<string>()))
                .Returns((string name) => Task.FromResult(new Products(testData.Where(td => td.Name.Contains(name)).ToList())));

            //Mock method CreateProduct(product):-
            mock.Setup(m => m.CreateProduct(It.IsAny<Product>()))
                .Callback((Product p) => testData.Add(p));

            //Mock method UpdateProduct(product):-
            mock.Setup(m => m.UpdateProduct(It.IsAny<Product>()))
                .Callback((Product p) => 
                {
                    var item = testData.Single(td => td.Id == p.Id);

                    item.Name = p.Name;
                    item.Description = p.Description;
                });

            //Mock method DeleteProduct(id):-
            mock.Setup(m => m.DeleteProduct(It.IsAny<Guid>()))
                .Callback((Guid id) => testData.RemoveAll(td => td.Id == id));

            return mock;
        }
    }
}
