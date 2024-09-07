using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using XeroProducts.DAL.Interfaces;
using XeroProducts.Types;

namespace XeroProducts.DAL.Sql.Providers
{
    public class UserSqlProvider : BaseSqlProvider, IUserDALProvider
    {
        public UserSqlProvider(Lazy<IConfiguration> configuration) : base(configuration)
        {
        }

        public virtual async Task<bool> UserExists(string username)
        {
            //Attempt to bring back any user that matches the username

            using (var connection = NewConnection())
            {
                var cmd = new SqlCommand($"SELECT * FROM [user] WHERE LOWER([Username]) = '{username.ToLower().Trim()}'", connection);
                await connection.OpenAsync();

                var rdr = await cmd.ExecuteReaderAsync();
                
                //if we dont find anyone that matches the username, the user doesnt already exist
                if (!await rdr.ReadAsync())
                {
                    return false;
                }
            }

            //if we get here, a user with this username does already exist
            return true;
        }

        public virtual async Task<User?> GetUser(Guid Id)
        {
            using (var connection = NewConnection())
            {
                var sql = $"SELECT [ID], [FirstName], [LastName], [Email], [Username], [HashedPassword], [Salt], [IsSuperAdmin] FROM [user] WHERE [Id] = '{Id}'";
                var cmd = new SqlCommand(sql, connection);
                await connection.OpenAsync();

                var rdr = await cmd.ExecuteReaderAsync();
                if (!await rdr.ReadAsync())
                {
                    return null;
                }

                return new Types.User()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Username = rdr["Username"].ToString(),
                    HashedPassword = rdr["HashedPassword"].ToString(),
                    Salt = rdr["Salt"].ToString(),
                    IsSuperAdmin = bool.Parse(rdr["IsSuperAdmin"].ToString())
                };
            }
        }

        public virtual async Task<User?> GetUser(string username)
        {
            using (var connection = NewConnection())
            {
                var sql = $"SELECT [ID], [FirstName], [LastName], [Email], [Username], [HashedPassword], [Salt], [IsSuperAdmin] FROM [user] WHERE LOWER([Username]) = '{username.ToLower().Trim()}'";
                var cmd = new SqlCommand(sql, connection);
                await connection.OpenAsync();

                var rdr = await cmd.ExecuteReaderAsync();
                if (!await rdr.ReadAsync())
                {
                    return null;
                }

                return new Types.User()
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Username = rdr["Username"].ToString(),
                    HashedPassword = rdr["HashedPassword"].ToString(),
                    Salt = rdr["Salt"].ToString(),
                    IsSuperAdmin = bool.Parse(rdr["IsSuperAdmin"].ToString())
                };
            }
        }

        public virtual async Task<Guid> CreateUser(User user)
        {
            using (var connection = NewConnection())
            {
                var sql = $"INSERT INTO [user] ([Id], [FirstName], [LastName], [Email], [Username], [HashedPassword], [Salt], [IsSuperAdmin] )"
                        + $"VALUES ('{user.Id}', '{user.FirstName.Trim()}', '{user.LastName.Trim()}', '{user.Email.Trim()}', '{user.Username.Trim()}', '{user.HashedPassword}', '{user.Salt}', 0 )";
                var cmd = new SqlCommand(sql, connection);
                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return user.Id;
            }
        }

        public virtual async Task UpdateUser(User user)
        {
            using (var connection = NewConnection())
            {
                var sql = $"UPDATE [user] "
                        + $"SET [FirstName] = '{user.FirstName.Trim()}', "
                        + $"    [LastName] = '{user.LastName.Trim()}', "
                        + $"    [Email] = '{user.Email.Trim()}', "
                        + $"    [Username] = '{user.Username.Trim()}', "
                        + $"    [HashedPassword] = '{user.HashedPassword}', "
                        + $"    [Salt] = '{user.Salt}' "
                        + $"WHERE [Id] = '{user.Id}' ";
                var cmd = new SqlCommand(sql, connection);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public virtual async Task DeleteUser(Guid id)
        {
            using (var connection = NewConnection())
            {
                var cmd = new SqlCommand($"DELETE FROM [user] WHERE [Id] = '{id}'", connection);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
