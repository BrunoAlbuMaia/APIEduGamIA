using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service;
using Shared.Requests;

namespace APIEduGamIA.Controllers
{
    [ApiController]
    [Route("/api/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        #region
        [HttpPost]
        #endregion
        public async Task<IActionResult> postLoginAsync([FromBody] LoginRequests login)
        {
            return await _authService.LoginAsync(login.username, login.password);
        }
    }
}
