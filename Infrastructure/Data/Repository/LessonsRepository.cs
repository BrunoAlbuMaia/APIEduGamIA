using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Connections;
using Shared.Dtos.Responses;
using Shared.Response;

namespace Infrastructure.Data.Repository
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly IDbEducacionalConnectionFactory _dbEducacional;
        public LessonsRepository(IDbEducacionalConnectionFactory dbEducacional)
        {
            _dbEducacional = dbEducacional;
        }
        public async Task<PaginatedResponse<LessonsEntity>> GetAllByModuleId(int moduleId, int page, int size,string title)
        {
            using (var connection = _dbEducacional.CreateConnection())
            {
                if (connection == null)
                {
                    throw new InvalidOperationException("Falha ao criar a conexão com o banco de dados.");
                }
                connection.Open();

                // Calculando o OFFSET
                int offset = (page - 1) * size;

                string countQuery = "SELECT COUNT(*) FROM lessons";

                if (!string.IsNullOrEmpty(title))
                {
                    countQuery += " WHERE title LIKE @title AND moduleId = @moduleId";
                }

                int total = await connection.ExecuteScalarAsync<int>(countQuery, new { title = $"%{title}%",moduleId = moduleId });

                var query = @"
                                SELECT 
                                    *

                                FROM lessons";
                if (!string.IsNullOrEmpty(title))
                {
                    query += " WHERE title LIKE @title and moduleId = @moduleId";
                }

                query += " ORDER BY id LIMIT @size OFFSET @Offset";

                var result = await connection.QueryAsync<LessonsEntity>(query,
                        new { Name = $"%{title}%", moduleId = moduleId, Size = size, Offset = offset });

                return new PaginatedResponse<LessonsEntity>(page, size, total, result.ToList());
            }
        }

        public async Task<bool> PostLessons(LessonsEntity entity)
        {
            using (var connection = _dbEducacional.CreateConnection())
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
                        var queryLessons = @"
                                                INSERT INTO 
                                                dbEducacional.lessons(moduleId,title,content,sort_order,content_type,media_url,user_create,user_update)
                                                VALUES(@moduleId,@title,@content,@sort_order,@content_type,@media_url,@user_create,@user_update)
                                              ss ";

                        var parametersLessons = new
                        {
                            moduleId = entity.moduleId,
                            title = entity.title,
                            content= entity.content,
                            sort_order = entity.sort_order,
                            content_type = entity.content_type,
                            media_url = entity.media_url,
                            user_create = entity.user_create,
                            user_update = entity.user_update
                        };


                        int courseId = await connection.QuerySingleAsync<int>(queryLessons, parametersLessons, transaction);

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
    }
}
