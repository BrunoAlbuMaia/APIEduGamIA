using Domain.Entitys;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RoomManagementAPI.API;
using Service;
using Shared.Dtos.Responses;
using Shared.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        #region
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastro para novos usúarios",
            Description = "Cria um novo usúario para pode utilizar as funcionalidades da aplicação "
        )]
        [SwaggerResponse(200, "Usúario cadastrado com sucesso", typeof(bool))]
        [SwaggerResponse(401, "Credenciais inválidas.")]
        #endregion
        public async Task<IActionResult> PostUsers(UsersRequests users)
        {
       
            return Ok(await _usersService.CreateUserAsync(users));
        }

        #region
        [HttpPut]
        [CustomAuthorize("STUDENT","TEACHER")]
        #endregion
        public async Task<IActionResult> PutUsers()
        {
            return Ok("");
        }
    }
}
