
using System.Data;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Context
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;

        public DatabaseConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString); // Cria a conexão com o banco
        }
    }
}
