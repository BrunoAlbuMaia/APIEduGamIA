using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class AuthenticationRequests
    {
    }

    public class LoginRequests
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
