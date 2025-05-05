using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RoomManagementAPI.API;
using Shared.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("/api/course")]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseSerivce)
        {
            _courseService = courseSerivce;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todos os cursos cadastrados na instituição")]
        public async Task<IActionResult> GetAllCourses(int page = 1,int size = 10, string? name = null)
        {
            var result = await _courseService.GetAllAsync(page, size, name);
            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um NOVO curso na instituição")]
        public async Task<IActionResult> PostCourse(CoursePostRequests courses)
        {
            courses.setUsername(HttpContext.Items["Username"].ToString());
            return Ok(await _courseService.CreateCourseAsyn(courses));
        }

        [HttpPost("{id}/upload-image")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Faz upload da imagem do curso")]
        public async Task<IActionResult> UploadCourseImage(int id, [FromForm] CoursePostImageRequests file)
        {
            if (file == null || file.File.Length == 0)
                return BadRequest("Imagem inválida.");
            var result = await _courseService.SetCourseImageAsync(id,file);
            return Ok(result);
        }

    }
}
