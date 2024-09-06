using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XeroProducts.BL.Dtos.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public bool IsNew { get; }

        public ProductDto(bool isNew = true)
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Price = decimal.Zero;
            DeliveryPrice = decimal.Zero;
            IsNew = isNew;
        }

        public ProductDto(Types.Product product)
        {
            Id = product.Id;
            IsNew = product.IsNew;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            DeliveryPrice = product.DeliveryPrice;
        }

        public Types.Product ToType()
        {
            return new Types.Product(IsNew)
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price,
                DeliveryPrice = DeliveryPrice
            };
        }
    }
}
