using System.Data;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Context
{
    public interface IDbCoreConnectionFactory
    {
        IDbConnection CreateConnection();
    }
    public class DbCoreConnectionFactory : IDbCoreConnectionFactory
    {
        private readonly string _connectionString;
        public DbCoreConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
