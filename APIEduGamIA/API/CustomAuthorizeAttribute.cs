using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Threading.Tasks;

namespace RoomManagementAPI.API
{

    public class CustomAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authorizationService = context.HttpContext.RequestServices.GetService<IAuthenticationService>();
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var (isAuthenticated, role, filialId, userId, username) = await authorizationService.ValidateAndAuthorizeAsync(token);

            if (!isAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (_roles.Length > 0 && !_roles.Contains(role))
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Você não tem permissão para executar esta ação."
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            context.HttpContext.Items["FilialId"] = filialId;
            context.HttpContext.Items["UserId"] = userId;
            context.HttpContext.Items["Username"] = username;

            await next();
        }
       
    }

}
