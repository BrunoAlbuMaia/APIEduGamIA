using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.Dtos.Responses;
using Shared.Requests;
using Shared.Response;

namespace Infrastructure.Data.Interfaces
{
    public interface ICoursesRepository
    {
        Task<PaginatedResponse<CoursesEntity>> GetAllAsync();


        Task<bool> UpdateCourseAsync(CoursesEntity course);

        Task<CoursePostResponse> CreateCourse(CoursesEntity course);
    }
}
