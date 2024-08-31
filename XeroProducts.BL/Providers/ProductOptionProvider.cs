using Microsoft.Data.SqlClient;
using XeroProducts.BL.Helpers;
using XeroProducts.Types;

namespace XeroProducts.BL.Providers
{
    public class ProductOptionProvider
    {
        public ProductOption GetProductOption(Guid id)
        {
            var conn = SqlHelper.NewConnection();
            var cmd = new SqlCommand($"select * from productoption where id = '{id}'", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
            {
                return new ProductOption()
                {
                    Id = id
                };
            }

            var option = new ProductOption(isNew: false)
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString()
            };

            conn.Close();

            return option;
        }


        public ProductOptions GetProductOptions(Guid productId)
        {
            var where = $"where productid = '{productId}'";

            var items = new List<ProductOption>();
            var conn = SqlHelper.NewConnection();
            var cmd = new SqlCommand($"select id from productoption {where}", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                items.Add(this.GetProductOption(id));
            }

            var options = new ProductOptions(items);

            conn.Close();

            return options;
        }

        public void Save(ProductOption productOption)
        {
            var conn = SqlHelper.NewConnection();
            var cmd = productOption.IsNew ?
                new SqlCommand($"insert into productoption (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')", conn) :
                new SqlCommand($"update productoption set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}'", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Delete(Guid productOptionId)
        {
            var conn = SqlHelper.NewConnection();
            conn.Open();
            var cmd = new SqlCommand($"delete from productoption where id = '{productOptionId}'", conn);
            cmd.ExecuteReader();
            conn.Close();
        }
    }
}
