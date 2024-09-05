using Microsoft.Data.SqlClient;
using XeroProducts.DAL.Interfaces;
using XeroProducts.DAL.Sql.Helpers;
using XeroProducts.Types;

namespace XeroProducts.DAL.Sql.Providers
{
    public class ProductOptionSqlProvider : IProductOptionDALProvider
    {
        public async Task<ProductOption> GetProductOption(Guid productOptionId)
        {
            using (var connection = SqlHelper.NewConnection())
            {
                var cmd = new SqlCommand($"select * from productoption where id = '{productOptionId}'", connection);

                await connection.OpenAsync();
                var rdr = await cmd.ExecuteReaderAsync();

                if (!await rdr.ReadAsync())
                {
                    return new ProductOption()
                    {
                        Id = productOptionId
                    };
                }

                return new ProductOption(isNew: false)
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = DBNull.Value == rdr["Description"] ? null : rdr["Description"].ToString()
                };
            }
        }

        public async Task<ProductOptions> GetProductOptions(Guid productId)
        {
            using (var connection = SqlHelper.NewConnection())
            {
                var where = $"where productid = '{productId}'";

                var items = new List<ProductOption>();

                var cmd = new SqlCommand($"select id from productoption {where}", connection);
                
                await connection.OpenAsync();
                var rdr = await cmd.ExecuteReaderAsync();

                while (await rdr.ReadAsync())
                {
                    var id = Guid.Parse(rdr["id"].ToString());
                    items.Add(await this.GetProductOption(id));
                }

                return new ProductOptions(items);
            }
        }

        public async Task<Guid> CreateProductOption(ProductOption productOption)
        {
            using (var connection = SqlHelper.NewConnection())
            {
                var cmd = new SqlCommand($"insert into productoption (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')", connection);
                  
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return productOption.Id;
            }
        }

        public async Task UpdateProductOption(ProductOption productOption)
        {
            using (var connection = SqlHelper.NewConnection())
            {
                var cmd = new SqlCommand($"update productoption set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}'", connection);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteProductOption(Guid productOptionId)
        {
            using (var connection = SqlHelper.NewConnection())
            {
                var cmd = new SqlCommand($"delete from productoption where id = '{productOptionId}'", connection);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
