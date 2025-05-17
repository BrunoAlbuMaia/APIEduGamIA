using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LessonsEntity
    {
        public int id { get; set; }
        public int moduleId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int sort_order { get; set; }
        public string content_type { get; set; }
        public string media_url { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? user_create { get; set; }
        public string? user_update { get; set; }

    }
}
