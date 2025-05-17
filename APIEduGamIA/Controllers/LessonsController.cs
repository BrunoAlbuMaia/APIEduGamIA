using Domain.Interfaces;
using Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("api/lessons")]
    public class LessonsController:ControllerBase
    {
        private readonly ILessonsService _lessonsService;
        public LessonsController(ILessonsService lessons)
        {
            _lessonsService = lessons;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLessonsByModule(int moduleId,int page = 1, int size = 10, string? title = null)
        {
            var result = await _lessonsService.GetAll(moduleId, page, size, title);
            return Ok(result);
        }
        
    }
}
