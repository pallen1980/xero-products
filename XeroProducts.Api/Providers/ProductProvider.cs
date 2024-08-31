﻿using Microsoft.Data.SqlClient;
using XeroProducts.Models;
using XeroProducts.Types;

namespace XeroProducts.Api.Providers
{
    public class ProductProvider
    {
        private ProductOptionProvider _productOptionProvider => new ProductOptionProvider();

        public Product GetProduct(Guid id)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"select * from product where id = '{id}'", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
                return null;

            return new Product(isNew: false)
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                Price = decimal.Parse(rdr["Price"].ToString()),
                DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
            };
        }

        public Products GetProducts()
        {
            return this.GetProducts(null);
        }

        public Products GetProducts(string name)
        {
            var where = string.IsNullOrEmpty(name) ? 
                            "" 
                            : $"where lower(name) like '%{name.Trim().ToLower()}%'";

            var items = new List<Product>();
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"select id from product {where}", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                items.Add(this.GetProduct(id));
            }

            return new Products(items);
        }

        public void Save(Product product)
        {
            var conn = Helpers.NewConnection();
            var cmd = product.IsNew ?
                new SqlCommand($"insert into product (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})", conn) :
                new SqlCommand($"update product set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}'", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid productId)
        {
            foreach (var option in _productOptionProvider.GetProductOptions(productId).Items)
                _productOptionProvider.Delete(option.Id);

            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = new SqlCommand($"delete from product where id = '{productId}'", conn);
            cmd.ExecuteNonQuery();
        }
    }
}
