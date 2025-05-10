using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class DisciplineRequests
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public string? Username { get; private set; }

        public void setUsername(string username) { Username = username; }
    }
}
