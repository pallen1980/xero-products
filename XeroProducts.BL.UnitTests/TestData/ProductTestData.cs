using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeroProducts.Types;

namespace XeroProducts.BL.UnitTests.TestData
{
    internal class ProductTestData
    {
        internal static List<Product> Create(int noOfProducts = 5)
        {
            var products = new List<Product>();

            for (var i=0; i<noOfProducts; i++)
            {
                products.Add(
                    new Product() 
                    { 
                        Id = Guid.NewGuid(), 
                        Name = string.Format("Product {0}",i), 
                        Description = string.Format("Product Description {0}", i), 
                        DeliveryPrice = 1.0M, 
                        Price = 100.00M 
                    });
            }

            return products;
        }
    }
}
