using System.Xml.Linq;
using Dapper;
using Domain.Entitys;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories.Interfaces;
using Shared.Dtos.Responses;


namespace Infrastructure.Data.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public UserRepository(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        } 
        public async Task<bool> RegisterUser(UsersEntity users)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new InvalidOperationException("Falha ao criar a conexão com o banco de dados.");
                    }
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            //Inserindo na tabela Principal
                            var queryUsers = @"INSERT INTO dbEduGamIa.users (firstName, lastName, is_active)
                                      VALUES (@firtsName,@lastName,1);
                                      SELECT LAST_INSERT_ID();";
                            
                            var parametersUsers = new
                            {
                                firtsName = users.firstName,
                                lastName = users.lastName
                            };

                            var userId = await connection.ExecuteScalarAsync<int>(queryUsers, parametersUsers, transaction);

                            //Inserindo na tabela Complemento
                            var queryUsersCompl = @"INSERT INTO dbEduGamIa.usersCompl(user_id,email,username,role,password_hash,preferences)
                                                   VALUES (@user_id,@email,@username,@role,@password_hash,@preferences);";

                            var parametersUsersCompl = new { 
                                user_id = userId,
                                email = users.UserCompl.email,
                                username = users.UserCompl.username,
                                role = users.UserCompl.role,
                                password_hash = users.UserCompl.password_hash,
                                preferences = users.UserCompl.preferences
                            };
                            await connection.ExecuteAsync(queryUsersCompl, parametersUsersCompl, transaction);
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public async Task<UsersEntity> UserByUsername(string username)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                if (connection == null)
                {
                    throw new InvalidOperationException("Falha ao connectar ao banco de dados");
                }
                connection.Open();

                var query = @"
                                SELECT 
                                    *
                                FROM users
                                INNER JOIN `usersCompl`
                                    ON users.id = `usersCompl`.user_id
                                WHERE 
                                    `usersCompl`.username = @username
                             ";
                var result = (await connection.QueryAsync<UsersEntity, UsersComplEntity, UsersEntity>(
                                                query,
                                                (user, userCompl) =>
                                                {
                                                    user.UserCompl = userCompl;
                                                    return user;
                                                },
                                                new { username },
                                                splitOn: "id" // 'id' da segunda tabela (usersCompl)
                                            )).FirstOrDefault();
                return result;

                
            }

        }
    }
}
