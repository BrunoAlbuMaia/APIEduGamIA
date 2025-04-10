using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<dynamic> LoginAsync(string username, string password);

        Task<(bool IsAuthenticated, string Role, string FilialId, string UserId, string Username)> ValidateAndAuthorizeAsync(string token);
    
    }
}
