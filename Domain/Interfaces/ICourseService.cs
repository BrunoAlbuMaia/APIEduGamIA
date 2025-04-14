using Shared.Requests;
using Shared.Response;

namespace Domain.Interfaces
{
    public interface ICourseService
    {
        Task<CoursePostResponse> CreateCourseAsyn(CoursePostRequests course);
        Task<bool> SetCourseImageAsync(int id,CoursePostImageRequests file);
    }
}
