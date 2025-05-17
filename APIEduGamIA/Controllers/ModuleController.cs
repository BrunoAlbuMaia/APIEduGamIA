using System.Drawing;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Response;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("api/module")]
    public class ModuleController : Controller
    {
        private readonly IModuleService _moduleService;
        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int size =  10, string? name = null)
        {
            var result = await _moduleService.GetAll(page, size, name);
            return Ok(result);
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(ModuleRequests module)
        //{
        //    module.setUsername("bruno.maia");
        //    var result = await _moduleService.Create(module);
        //    return Ok(result);

        //}
    }
}
