using Microsoft.AspNetCore.Mvc;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Hello World");
        }
    }
}
