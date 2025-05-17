using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Requests
{
    public class LessonsRequests
    {
        public int moduleId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int sort_order { get; set; }
        public string content_type { get; set; }
        public string media_url { get; set; }

    }
}
