using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XeroProducts.BL.Dtos.Product
{
    public class ProductOptionDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsNew { get; }

        public ProductOptionDto(bool isNew = true) 
        {
            Id = Guid.Empty;
            ProductId = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            IsNew = isNew;
        }

        public ProductOptionDto(Types.ProductOption productOption)
        {
            Id = productOption.Id;
            ProductId = productOption.ProductId;
            IsNew = productOption.IsNew;
            Name = productOption.Name;
            Description = productOption.Description;
        }

        public Types.ProductOption ToType()
        {
            return new Types.ProductOption(IsNew)
            {
                Id = Id,
                ProductId = ProductId,
                Name = Name,
                Description = Description
            };
        }
    }
}
