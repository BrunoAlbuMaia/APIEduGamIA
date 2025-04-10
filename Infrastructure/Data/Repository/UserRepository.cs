using Dapper;
using Domain.Entitys;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories.Interfaces;


namespace Infrastructure.Data.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public UserRepository(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public async Task<UserEntity> UserByUsername(string username)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
               
                    var query = "SELECT * FROM Users WHERE Username = @Username";
                    var parameters = new { Username = username };

                    var user = await connection.QueryFirstOrDefaultAsync<UserEntity>(query, parameters);
                    return user;
               
            }
        }
    }
}
