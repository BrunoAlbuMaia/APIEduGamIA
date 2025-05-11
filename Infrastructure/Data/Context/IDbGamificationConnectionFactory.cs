
using System.Data;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Context
{
    public interface IDbGamificationConnectionFactory
    {
        IDbConnection CreateConnection();
    }
    public class DbGamificationConnectionFactory : IDbGamificationConnectionFactory
    {
        private readonly string _connectionString;
        public DbGamificationConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
