using Domain.Entitys;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RoomManagementAPI.API;
using Service;
using Shared.Requests;

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
