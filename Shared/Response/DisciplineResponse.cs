using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Response
{
    public class DisciplineResponse
    {
    }

    public class DisciplinePostResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
