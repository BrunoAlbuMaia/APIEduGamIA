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

namespace Infrastructure.Data.Repository
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly IDbEducacionalConnectionFactory _connectionFactory;
        public QuestionsRepository(IDbEducacionalConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<PaginatedResponse<QuestionsEntity>> GetAll(int lessonsId, int page, int size)
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

                string countQuery = "SELECT COUNT(*) FROM questions WHERE lessonId = @lessonsId";

               

                int total = await connection.ExecuteScalarAsync<int>(countQuery, new { lessonsId = lessonsId });

                var query = @"
                      SELECT 
                          *

                      FROM questions
                      WHERE lessonId = @lessonsId";
           

                query += " ORDER BY sort_order LIMIT @size OFFSET @Offset";

                var result = await connection.QueryAsync<QuestionsEntity>(query,
                        new { lessonsId =lessonsId, Size = size, Offset = offset });

                return new PaginatedResponse<QuestionsEntity>(page, size, total, result.ToList());
            }
        }
    }
}
