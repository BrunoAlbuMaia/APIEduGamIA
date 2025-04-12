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
            Summary = "Cadastro para novos us�arios",
            Description = "Cria um novo us�ario para pode utilizar as funcionalidades da aplica��o "
        )]
        [SwaggerResponse(200, "Us�ario cadastrado com sucesso", typeof(bool))]
        [SwaggerResponse(401, "Credenciais inv�lidas.")]
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
