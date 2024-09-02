using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace XeroProducts.Types
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        [JsonIgnore]
        public bool IsNew { get; }

        public Product(bool isNew = true)
        {
            Id = Guid.NewGuid();
            IsNew = isNew;
            Name = "";
            Description = "";
            Price = 0.0M;
            DeliveryPrice = 0.0M;
        }
    }
}
