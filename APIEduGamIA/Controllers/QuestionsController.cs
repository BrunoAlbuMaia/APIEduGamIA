using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionsService _questionsService;
        public QuestionsController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestion(int lessionId, int page = 1, int size= 10)
        {
            var result = await _questionsService.GetQuestionsAsync(lessionId, page, size);
            return Ok(result);
        }
    }
}
