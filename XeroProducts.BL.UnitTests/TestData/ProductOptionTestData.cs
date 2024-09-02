using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.BL.UnitTests.TestData
{
    internal class ProductOptionTestData
    {
        internal static List<ProductOption> Create(int productCount = 10)
        {
            var productIds = new List<Guid>(productCount);

            for (var i=0; i<productCount; i++)
            {
                productIds.Add(Guid.NewGuid());
            }
            
            return Create(productIds);
        }

        internal static List<ProductOption> Create(List<Guid> productIds)
        {
            var productOptions = new List<ProductOption>();

            //for each product id, add a random amount of options to the list
            var prodIndex = 0;
            foreach (var productId in productIds)
            {
                for (var i = 0; i < new Random().Next(2, 5); i++)
                {
                    productOptions.Add(
                        new ProductOption
                        {
                            Id = Guid.NewGuid(),
                            Name = string.Format("Product {0} - Option {1}", prodIndex, i),
                            Description = "Test Description",
                            ProductId = productId
                        });
                }
            }

            return productOptions;
        }
    }
}
