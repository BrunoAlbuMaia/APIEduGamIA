using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Interfaces;
using Shared.Dtos.Responses;
using Shared.Requests;
using Shared.Response;

namespace Service
{
    public class CourseService : ICourseService
    {
        private readonly ICoursesRepository _courseRepository;
        public CourseService(ICoursesRepository coursesRepository)
        {
            _courseRepository = coursesRepository;
        }

        public async Task<CoursePostResponse> CreateCourseAsyn(CoursePostRequests course)
        {
            var courseEntity = new CoursesEntity
            {
                name = course.name,
                description = course.description,
                is_active = true,
                user_create = course.Username,
                user_update = course.Username
            };
            return await _courseRepository.CreateCourse(courseEntity);
        }

        public async Task<bool> SetCourseImageAsync(int id,CoursePostImageRequests file)
        {
            var directory = @"D:\ImgCursos";
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.File.FileName)}";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var path = Path.Combine(directory, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.File.CopyToAsync(stream);
            }

            var courseEntity = new CoursesEntity
            {
                id = id,
                image = $"{fileName}"
            };
            var result = await _courseRepository.UpdateCourseAsync(courseEntity);

            return result;
        }

        public async Task<PaginatedResponse<CoursesEntity>> GetAllAsync(int page, int size, string? name = null)
        {
            var result = await _courseRepository.GetAllAsync(page,size,name);
            return result;
        }
    }
}
