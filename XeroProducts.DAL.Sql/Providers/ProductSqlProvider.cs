using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.DAL.Sql.Providers
{
    public class ProductSqlProvider : BaseSqlProvider, IProductDALProvider
    {
        public ProductSqlProvider(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public async Task<Product?> GetProduct(Guid id)
        {
            using (var connection = NewConnection())
            {
                var cmd = new SqlCommand($"select * from product where id = '{id}'", connection);
                await connection.OpenAsync();

                var rdr = await cmd.ExecuteReaderAsync();
                if (!await rdr.ReadAsync())
                {
                    return null;
                }

                return new Product(isNew: false)
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = DBNull.Value == rdr["Description"] ? null : rdr["Description"].ToString(),
                    Price = decimal.Parse(rdr["Price"].ToString()),
                    DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
                };
            }
        }

        public async Task<Products> GetProducts(string name = "")
        {
            using (var connection = NewConnection())
            {
                var where = string.IsNullOrEmpty(name) ?
                            ""
                            : $"where lower(name) like '%{name.Trim().ToLower()}%'";

                var items = new List<Product>();

                var cmd = new SqlCommand($"select id from product {where}", connection);
                await connection.OpenAsync();

                var rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    var id = Guid.Parse(rdr["id"].ToString());
                    items.Add(await GetProduct(id));
                }

                return new Products(items);
            }
        }

        public async Task<Guid> CreateProduct(Product product)
        {
            using (var connection = NewConnection())
            {
                var cmd = new SqlCommand($"insert into product (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})", connection);
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return product.Id;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            using (var connection = NewConnection())
            {
                var cmd = new SqlCommand($"update product set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}'", connection);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteProduct(Guid id)
        {
            using (var connection = NewConnection())
            {
                var cmd = new SqlCommand($"delete from product where id = '{id}'", connection);
                
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
