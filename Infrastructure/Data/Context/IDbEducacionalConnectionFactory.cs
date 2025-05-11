using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Context
{
    public interface IDbEducacionalConnectionFactory
    {
        IDbConnection CreateConnection();
    }
    public class DbEducacionalConnectionFactory : IDbEducacionalConnectionFactory
    {
        private readonly string _connectionString;
        public DbEducacionalConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
