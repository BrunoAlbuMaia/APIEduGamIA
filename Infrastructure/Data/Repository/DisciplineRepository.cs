using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interfaces;
using Shared.Dtos.Responses;
using Shared.Response;

namespace Infrastructure.Data.Repository
{
    public class DisciplineRepository : IDisciplineRepository
    {
        public readonly IDbEducacionalConnectionFactory _connectionFactory;

        public DisciplineRepository(IDbEducacionalConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<DisciplinePostResponse> Create(DisciplineEntity disciplina)
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
                        var queryDisciplina = @"INSERT INTO discipline (name,description,is_active,user_create,user_update)
                        VALUES (@name,@description,1,@user_create,@user_update);
                        SELECT LAST_INSERT_ID();";

                        var parametersDisciplina = new
                        {
                            name = disciplina.name,
                            description = disciplina.description,
                            user_create = disciplina.user_create,
                            user_update = disciplina.user_update,
                        };


                        int courseId = await connection.QuerySingleAsync<int>(queryDisciplina, parametersDisciplina, transaction);

                        transaction.Commit();

                        return new DisciplinePostResponse { 
                            name = disciplina.name,
                            description = disciplina.description,
                            //user_create = disciplina.user_create,
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

        public async Task<PaginatedResponse<DisciplineEntity>> GetAll(int page, int size, string? name = null)
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

                string countQuery = "SELECT COUNT(*) FROM discipline";

                if (!string.IsNullOrEmpty(name))
                {
                    countQuery += " WHERE name LIKE @name";
                }

                int total = await connection.ExecuteScalarAsync<int>(countQuery, new { name = $"%{name}%" });

                var query = @"
                                SELECT 
                                    *

                                FROM discipline";
                if (!string.IsNullOrEmpty(name))
                {
                    query += " WHERE discipline.name LIKE @name";
                }

                query += " ORDER BY discipline.id LIMIT @size OFFSET @Offset";

                var result = await connection.QueryAsync<DisciplineEntity>(query,
                        new { Name = $"%{name}%", Size = size, Offset = offset });

                return new PaginatedResponse<DisciplineEntity>(page, size, total, result.ToList());
            }
        }

        public Task<DisciplineEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(DisciplineEntity disciplina)
        {
            throw new NotImplementedException();
        }
    }
}
