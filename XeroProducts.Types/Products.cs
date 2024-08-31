using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroProducts.Types
{
    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products(List<Product> items)
        {
            Items = items;
        }

    }
}
