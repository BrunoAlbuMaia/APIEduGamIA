using Dapper;
using Domain.Entities;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interfaces;
using MySql.Data.MySqlClient;
using Shared.Dtos.Responses;
using Shared.Response;
using static Dapper.SqlMapper;

namespace Infrastructure.Data.Repository
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public CoursesRepository(IDatabaseConnectionFactory db)
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
                            var queryCourse = @"INSERT INTO dbEduGamIa.cursos (name, description,isActive,user_create,user_update)
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

        public Task<PaginatedResponse<CoursesEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
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

                if (course.isActive != null)
                {
                    fieldsToUpdate.Add("is_active = @isActive");
                    parameters.Add("@isActive", course.isActive);
                }

                if (course.user_update != null)
                {
                    fieldsToUpdate.Add("updated_by = @userUpdate");
                    parameters.Add("@userUpdate", course.user_update);
                }

                fieldsToUpdate.Add("updated_at = NOW()");

                if (!fieldsToUpdate.Any())
                    return false;
                using (var transaction = connection.BeginTransaction())
                {
                    try { 
                        var sql = $"UPDATE courses SET {string.Join(", ", fieldsToUpdate)} WHERE id = @id";
                        var result = await connection.ExecuteAsync(sql, parameters, transaction);
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
