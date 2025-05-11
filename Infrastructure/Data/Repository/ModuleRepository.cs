using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Connections;
using Shared.Dtos.Responses;
using Shared.Response;

namespace Infrastructure.Data.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IDbEducacionalConnectionFactory _connectionFactory;

        public ModuleRepository(IDbEducacionalConnectionFactory connection)
        {
            _connectionFactory = connection;
        }
        public async Task<ModulePostResponse> Create(ModuleEntity module)
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
                        var querymodule = @"INSERT INTO module (name,description,is_active,user_create,user_update)
                        VALUES (@name,@description,1,@user_create,@user_update);
                        SELECT LAST_INSERT_ID();";

                        var parametersmodule = new
                        {
                            name = module.name,
                            description = module.description,
                            user_create = module.user_create,
                            user_update = module.user_update,
                        };


                        int moduleId = await connection.QuerySingleAsync<int>(querymodule, parametersmodule, transaction);

                        transaction.Commit();

                        return new ModulePostResponse
                        {
                            id = moduleId,
                            name = module.name,
                            description = module.description,
                        };
                        
                           
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<PaginatedResponse<ModuleEntity>> GetAll(int page, int size, string? name = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                if (connection == null)
                {
                    throw new InvalidOperationException("Falha ao criar a conexão com o banco de dados.");
                }
                connection.Open();

                // Calculando o OFFSET
                int offset = (page - 1) * size;

                string countQuery = "SELECT COUNT(*) FROM module";

                if (!string.IsNullOrEmpty(name))
                {
                    countQuery += " WHERE name LIKE @name";
                }

                int total = await connection.ExecuteScalarAsync<int>(countQuery, new { name = $"%{name}%" });

                var query = @"
                                SELECT 
                                    *

                                FROM module";
                if (!string.IsNullOrEmpty(name))
                {
                    query += " WHERE name LIKE @name";
                }

                query += " ORDER BY id LIMIT @size OFFSET @Offset";

                var result = await connection.QueryAsync<ModuleEntity>(query,
                        new { Name = $"%{name}%", Size = size, Offset = offset });

                return new PaginatedResponse<ModuleEntity>(page, size, total, result.ToList());
            }
        }
    }
}
