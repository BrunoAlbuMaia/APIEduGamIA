using Microsoft.AspNetCore.Mvc;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("/api/discipline")]
    public class DisciplineController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int size = 10, string? name = null)
        {
            return Ok("");
        }
    }
}
