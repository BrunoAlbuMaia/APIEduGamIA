using Domain.Entities;
using Microsoft.Extensions.Hosting;
using Shared.Dtos.Responses;
using Shared.Requests;
using Shared.Response;

namespace Domain.Interfaces
{
    public interface ICourseService
    {
        Task<CoursePostResponse> CreateCourseAsyn(CoursePostRequests course);
        Task<bool> SetCourseImageAsync(int id,CoursePostImageRequests file);
        Task<PaginatedResponse<CoursesEntity>> GetAllAsync(int page, int size, string name = null);
    }
}
