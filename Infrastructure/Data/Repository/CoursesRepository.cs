using System.Drawing;
using Dapper;
using Domain.Entities;
using Domain.Entitys;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Shared.Dtos.Responses;
using Shared.Response;
using static Dapper.SqlMapper;

namespace Infrastructure.Data.Repository
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly IDbEducacionalConnectionFactory _connectionFactory;
        public CoursesRepository(IDbEducacionalConnectionFactory db)
        {
            _connectionFactory = db;
        }

        public async Task<CoursePostResponse> CreateCourse(CoursesEntity course)
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
                            var queryCourse = @"INSERT INTO course (name, description,is_active,user_create,user_update)
                                      VALUES (@name,@description,1,@user_create,@user_update);
                                      SELECT LAST_INSERT_ID();";

                            var parametersCourse = new
                            {
                                name = course.name,
                                description = course.description,
                                user_create = course.user_create,
                                user_update = course.user_update,
                            };


                            int courseId = await connection.QuerySingleAsync<int>(queryCourse, parametersCourse, transaction);

                            transaction.Commit();
                            return new CoursePostResponse {
                                id = courseId,
                                name = course.name,
                                description = course.description
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
            catch (Exception ex)
            {
                throw;

            }
        }

        public async Task<PaginatedResponse<CoursesEntity>> GetAllAsync(int page, int size, string? name = null)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new InvalidOperationException("Falha ao connectar ao banco de dados");
                    }
                    connection.Open();

                    // Calculando o OFFSET
                    int offset = (page - 1) * size;

                    string countQuery = "SELECT COUNT(*) FROM course";

                    if (!string.IsNullOrEmpty(name))
                    {
                        countQuery += " WHERE name LIKE @name";
                    }

                    int total = await connection.ExecuteScalarAsync<int>(countQuery, new { name = $"%{name}%" });

                    var query = @"
                                SELECT 
                                    *

                                FROM course";
                    if (!string.IsNullOrEmpty(name))
                    {
                        query += " WHERE course.name LIKE @name";
                    }

                    query += " ORDER BY course.id LIMIT @size OFFSET @Offset";

                    var result = await connection.QueryAsync<CoursesEntity>(query,
                            new { Name = $"%{name}%", Size = size, Offset = offset });
                    
                    return new PaginatedResponse<CoursesEntity>(page,size,total,result.ToList());


                }

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            
        }

        public async Task<bool> UpdateCourseAsync(CoursesEntity course)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                if (connection == null)
                {
                    throw new InvalidOperationException("Falha ao criar a conexão com o banco de dados.");
                }
                connection.Open();

                if (course.id == null)
                    throw new ArgumentException("O ID do curso é obrigatório.");

                var fieldsToUpdate = new List<string>();
                var parameters = new DynamicParameters();

                parameters.Add("@id", course.id);

                if (course.name != null)
                {
                    fieldsToUpdate.Add("name = @name");
                    parameters.Add("@name", course.name);
                }

                if (course.description != null)
                {
                    fieldsToUpdate.Add("description = @description");
                    parameters.Add("@description", course.description);
                }

                if (course.image != null)
                {
                    fieldsToUpdate.Add("image = @image");
                    parameters.Add("@image", course.image);
                }

                if (course.is_active != null)
                {
                    fieldsToUpdate.Add("is_active = @isActive");
                    parameters.Add("@isActive", course.is_active);
                }

                if (course.user_update != null)
                {
                    fieldsToUpdate.Add("updated_by = @userUpdate");
                    parameters.Add("@userUpdate", course.user_update);
                }

                

                if (!fieldsToUpdate.Any())
                    return false;
                using (var transaction = connection.BeginTransaction())
                {
                    try { 
                        var sql = $"UPDATE course SET {string.Join(", ", fieldsToUpdate)} WHERE id = @id";
                        var result = await connection.ExecuteAsync(sql, parameters, transaction);
                        transaction.Commit();
                        return result > 0;

                    } catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
        }
    }
}
