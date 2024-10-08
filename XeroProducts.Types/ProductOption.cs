﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XeroProducts.Types
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [JsonIgnore]
        public bool IsNew { get; }

        public ProductOption()
        {

        }

        public ProductOption(bool isNew = true)
        {
            Id = Guid.NewGuid();
            IsNew = isNew;
            Name = "";
            Description = "";
        }
    }
}
