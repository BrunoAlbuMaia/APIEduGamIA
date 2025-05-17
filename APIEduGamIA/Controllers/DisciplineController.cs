using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Response;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("/api/discipline")]
    public class DisciplineController : Controller
    {
        private readonly IDisciplineService _discipline;
        public DisciplineController(IDisciplineService discipline)
        {
            _discipline = discipline;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int size = 10, string? name = null)
        {
            var result = await _discipline.GetAll(page,size,name);
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(DisciplineRequests discipline)
        //{
        //    //discipline.setUsername(HttpContext.Items["Username"].ToString());
        //    discipline.setUsername("bruno.maia");
        //    var result = await _discipline.Create(discipline);
        //    return Ok(result);
        //}
    }
}
